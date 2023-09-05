using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Info : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI livesUI;
    [SerializeField] private TextMeshProUGUI currencyUI;

    private void OnGUI()
    {
        livesUI.text = LevelManager.main.lives.ToString();
        currencyUI.text = LevelManager.main.coins.ToString();
    }
}
