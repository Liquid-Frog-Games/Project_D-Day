using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;

    public static bool gameIsPaused = false;
    public GameObject gameOverMenuUI;
    public static UnityEvent e_GameOver = new UnityEvent();

    public int lives;
    public int coins;
    public TextMeshProUGUI notificationText;

    private void Awake()
    {
        main = this;
        e_GameOver.AddListener(GameOver);
    }

    private void Start()
    {
        lives = 100;
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
            Debug.Log("You don't have enough coins to purchase this item.");
            return false;
            
        }
    }

    public void StartNotification()
    {
        StartCoroutine(sendNotification("You dont have enough coins for this purchase", 3));   //Starts the notification below
    }

    public void GameOver()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public IEnumerator sendNotification(string text, int time)
    {
        Debug.Log("I work");
        notificationText.text = text;            //set the text in the screen to the given text
        yield return new WaitForSeconds(time);      //wait given seconds
        notificationText.text = "";                  //set text back to ""
    }
}
