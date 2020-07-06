using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
    [SerializeField] private PlayerLife playerLife;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject menuCanvas;

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

    public void Credits()
    {
        credits.SetActive(true);
        menuCanvas.SetActive(false);
    }

    public void CloseCredits()
    {
        credits.SetActive(false);
        menuCanvas.SetActive(true);
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
