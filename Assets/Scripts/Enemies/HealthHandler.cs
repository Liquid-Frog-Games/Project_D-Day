using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int currencyWorth = 50;
    public float hitPoints = 1f;

    private bool isDestroyed = false; 

    public void TakeDamage(float dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0f && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

}
