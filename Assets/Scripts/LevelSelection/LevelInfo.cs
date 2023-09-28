using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    public string targetLevel;

    public void LoadLevel()
    {
        SceneManager.LoadScene(targetLevel);
    }
}
