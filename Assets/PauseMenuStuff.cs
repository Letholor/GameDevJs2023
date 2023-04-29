using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuStuff : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void BackToMain()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Close()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}