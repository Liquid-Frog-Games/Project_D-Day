using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public EnemySpawner es;
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject playButton;
    public GameObject pauseButton;
    public Canvas UIcanvas;

    [SerializeField] PlayableDirector timeline;
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
        if (pauseMenu)
        {
            pauseMenu.SetActive(true);
        }

        if (playButton)
        {
            playButton.SetActive(true);
        }

        if (pauseButton)
        {
            pauseButton.SetActive(false);
        }
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private IEnumerator PlayCutscene()
    {
        UIcanvas.enabled = false;
        timeline.Play();
        yield return new WaitUntil(() => timeline.state == PlayState.Paused);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //Set to next screen
    }

    public void NextStage()
    {
        Time.timeScale = 1f;
        StartCoroutine(PlayCutscene());

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
