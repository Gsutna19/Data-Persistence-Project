using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{

    public static MainManager Instance { get; private set;}
    public string playerName;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text HighScoreText;
    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private int m_HighScore;
    private bool m_GameOver = false;

    void Start()
    {
        LoadHighScore();
        if (string.IsNullOrEmpty(GameManager.Instance.currentName))
        {
            playerName = GameManager.Instance.PlayerNameInputField.text;
            GameManager.Instance.currentName = playerName;
        }
        else
        {
            playerName = GameManager.Instance.currentName;
        }

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_GameOver = false;
                SceneManager.LoadScene(1);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string playerName;
    }
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = m_Points;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_HighScore = data.highScore;
            playerName = data.playerName;
            HighScoreText.text = $"High Score : {playerName} : {m_HighScore}";
        }
    }
    public void CheckHighScore()
    {
        if (m_HighScore == 0 || m_Points > m_HighScore)
        {
            m_HighScore = m_Points;
            HighScoreText.text = $"High Score : {playerName} : {m_HighScore}";
            SaveHighScore();
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        CheckHighScore();
        GameOverText.SetActive(true);
    }
}