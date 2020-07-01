using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject soundManager;

    private void Start()
    {
        soundManager = GameObject.Find("Sound Manager");
    }

    public void LoadMenu()
    {
        if (soundManager)
        {
            DontDestroyOnLoad(soundManager);
        }

        SceneManager.LoadScene("Menu");
    }

    public void LoadGameScene()
    {
        if (soundManager)
        {
            DontDestroyOnLoad(soundManager);
        }

        SceneManager.LoadScene("Game Scene");
    }

    public void LoadGameOverScene()
    {
        if (soundManager)
        {
            DontDestroyOnLoad(soundManager);
        }

        SceneManager.LoadScene("Game Over Scene");
    }

    public void LoadWinScene()
    {
        if (soundManager)
        {
            DontDestroyOnLoad(soundManager);
        }

        SceneManager.LoadScene("Win Scene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
