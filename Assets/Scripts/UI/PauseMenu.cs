using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public EnemySpawner es;
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

    public void NextStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       //Set to next screen
    }

    public void Freeplay()
    {
        pauseMenu.SetActive(false);
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;

        es.NextWave();
    }

    public void RestartStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("00_Main_Menu");
    }
}
