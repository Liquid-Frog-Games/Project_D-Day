using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

//Create a class for the waypoints, this is needed for the array in an array for pathing
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
    public List<GameObject> enemyList;

    public static bool gameIsPaused = false;
    public GameObject gameOverMenuUI;
    public static UnityEvent e_GameOver = new UnityEvent();
    public TextMeshProUGUI notificationText;

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
        notificationText.enabled = false; 
    }

    public void IncreaseCurrency(int amount)
    {
        coins += amount;
    }

    public void ResetHealth()
    {
        lives = 200f;
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
        notificationText.enabled = true;
        notificationText.text = text;            //set the text in the screen to the given text
        yield return new WaitForSeconds(time);      //wait given seconds
        notificationText.text = "";                  //set text back to ""
        notificationText.enabled = false;
    }

    public void StartNotification()
    {
        //starts notifcation above
        StartCoroutine(sendNotification("You don't have enough coins for this tower.", 3)); 
    }
}
