using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject cheaterMenu;
    public EnemySpawner enemySpawner;
    private int taps = 0;
    
    //amount of taps to open the menu
    public void OnTap()
    {
        if (taps >= 5)
        {
            OpenCheatMenu();
            return;
        }
        taps += 1;
        return;
    }

    //menu opens
    private void OpenCheatMenu()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        cheaterMenu.SetActive(true);
    }

    //resets health
    public void AddHealth()
    {
        LevelManager.main.ResetHealth();
    }

    //adds 200 coins
    public void AddCoins()
    {
        LevelManager.main.IncreaseCurrency(200);
    }

    //kills all enemies that have spawned
    public void KillAll()
    {

    //TODO KILL ALL ENEMIES 
    }

    //closes window
    public void CloseCheatMenu()
    {
        cheaterMenu.SetActive(false);
    }

}
