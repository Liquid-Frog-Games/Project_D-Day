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
        if (livesUI)
        {
            livesUI.text = Mathf.Round(LevelManager.main.lives).ToString();
        }

        if (currencyUI)
        {
            currencyUI.text = LevelManager.main.coins.ToString();
        }
    }
}
