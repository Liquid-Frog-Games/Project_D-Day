using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject playButton;
    public GameObject pauseButton;

    public void resume()
    {
        pauseMenu.SetActive(false);
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void pause()
    {
        pauseMenu.SetActive(true);
        playButton.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);       //set to screen 1 (or 2 if level selecter)
    }
}
