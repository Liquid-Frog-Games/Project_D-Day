using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public EnemyMovement em;

    [Header("Attributes")]
    [SerializeField] private int currencyWorth = 50;
    public float hitPoints = 1f;
    public float dmg = 1f;

    private bool isDestroyed = false; 

    //When hit by a bullet, take damage according to the bullet colliding
    public void TakeDamage(float dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0f && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            em.e_IsDead.Invoke();
        }
    }

}
