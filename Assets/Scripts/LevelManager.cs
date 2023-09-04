using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager main;
    public Transform startPoint;
    public Transform[] path;

    public int coins;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
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
            //BUY ITEM
            coins -= amount;
            return true;
        }
        else
        {
            Debug.Log("You don't have enough coins to purchase this item."); //TODO: Replace with UI message
            return false;
        }
    }
}
