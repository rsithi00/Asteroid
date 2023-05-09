using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      // Use this if you swap scenes

public class MainMenu : MonoBehaviour
{
    public void OnPlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitGame()
    {
        Debug.Log("Quitting game here.");
        Application.Quit();     // Won't work in Unity Editor, but will work on actual build.
        
    }
}
