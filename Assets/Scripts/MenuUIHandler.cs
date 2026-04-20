using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Execute this script after all others have been executed.
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public string playerName;

    // private void Start()
    // {
    //     MainManager.Instance.PlayerName = PlayerName;
    // }
    // public void NameSelected(Text PlayerName)
    // {
    //     MainManager.Instance.PlayerName = PlayerName;
    // }

    public void StartNew()
    {
        // playerName = GameManager.Instance.PlayerNameInputField.text;
        // MainManager.Instance.playerName = playerName;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif

        MainManager.Instance.SaveHighScore();
    }
}
