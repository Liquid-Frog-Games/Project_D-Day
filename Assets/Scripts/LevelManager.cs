using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;

    public static bool gameIsPaused = false;
    public GameObject gameOverMenuUI;
    public static UnityEvent e_GameOver = new UnityEvent();

    public float lives;
    public int coins;

    private void Awake()
    {
        main = this;
        e_GameOver.AddListener(GameOver);
    }

    private void Start()
    {
        lives = 100f;
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
}
