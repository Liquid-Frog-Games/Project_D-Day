using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PathChoice
{
    public Transform[] waypoints;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform startPoint;
    public PathChoice[] pathChoices;

    public static bool gameIsPaused = false;
    public GameObject gameOverMenuUI;
    public static UnityEvent e_GameOver = new UnityEvent();

    [Header("References")]
    public float lives;
    public int coins;

    private void Awake()
    {
        main = this;
        e_GameOver.AddListener(GameOver);
    }

    private void Start()
    {
        lives = 200f;
        coins = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        coins += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= coins)
        {
            coins -= amount;
            return true;
        }
        else
        {
            Debug.Log("You don't have enough coins to purchase this item."); //TODO: Replace with UI message
            return false;
        }
    }

    public void GameOver()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public IEnumerator sendNotification(string text, int time)
    {
        notificationText.text = text;            //set the text in the screen to the given text
        yield return new WaitForSeconds(time);      //wait given seconds
        notificationText.text = "";                  //set text back to ""
    }
}
