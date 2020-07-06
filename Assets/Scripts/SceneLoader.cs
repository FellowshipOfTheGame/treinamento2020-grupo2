using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AudioSource soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<AudioSource>();
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
