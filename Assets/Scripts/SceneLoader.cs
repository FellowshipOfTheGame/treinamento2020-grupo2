using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("Game Over Scene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
