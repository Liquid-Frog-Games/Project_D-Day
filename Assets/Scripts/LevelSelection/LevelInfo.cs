using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    public bool isLocked;
    public Button btn;
    public string targetLevel;

    private void Start()
    {
        //Get data from localstorage

        if (isLocked)
        {
            btn.interactable = false;
        }
    }

    public void LoadLevel()
    {
        Debug.Log("load level " + this);
        //Load the selected level (option, use dictionary to have a key(levelname(02_Level01) and a value(Level 01)))
        SceneManager.LoadScene(targetLevel);
    }
}
