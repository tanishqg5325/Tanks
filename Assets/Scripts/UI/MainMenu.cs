﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PlayGame ()
    {
        SceneManager.LoadScene("Main");
    }
    public void Training()
    {
        SceneManager.LoadScene("Training");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
