using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelInfo : MonoBehaviour
{
    public string targetLevel;
    public TextMeshProUGUI levelText;

    public void LoadLevel()
    {
        SceneManager.LoadScene(targetLevel);
    }

    public void ClearText()
    {
        levelText.text = "";
    }
}
