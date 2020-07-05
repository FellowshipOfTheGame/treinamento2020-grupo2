using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    [SerializeField] private PlayerLife playerLife;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (playerLife.IsDead)
        {
            //PauseGame();
            GameOverMenu();
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private void GameOverMenu()
    {
        gameOverMenuUI.SetActive(true);
    }

    public void LoadScene(string sceneName)
	{
		SceneManager.LoadSceneAsync(sceneName);
	}

	public void UnloadScene(string sceneName)
	{
		SceneManager.UnloadSceneAsync(sceneName);
	}

	// quit game
	public void ExitGame()
	{
		Application.Quit();
	}
}
