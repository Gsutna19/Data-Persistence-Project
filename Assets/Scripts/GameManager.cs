using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text HighScoreText;
    public Text ScoreText;
    public int hScore = 0;
    public string pName;
    public string currentName;
    public TMP_InputField PlayerNameInputField;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int highScore;
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            hScore = data.highScore;
            pName = data.playerName;
            // HighScoreText.text = $"High Score : {pName} : {hScore}";
        }
    }

    
}
