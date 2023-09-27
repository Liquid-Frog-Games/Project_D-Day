using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public GameObject cheaterMenu;
    public EnemySpawner es;
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

    //Adds 100 souls
    public void AddSouls()
    {
        Medusa.main.soulCount += 100;
    }

    //kills all enemies that have spawned (EDIT: just skip the round and go to the next one)
    public void KillAll()
    {
        EnemySpawner.main.EndWave();
    }

    //closes window
    public void CloseCheatMenu()
    {
        cheaterMenu.SetActive(false);
    }

}
