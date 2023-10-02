using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsHandler : MonoBehaviour
{
    private HealthHandler healthHandler;
    private EnemyMovement enemy;

    private List<int> tickTimer = new List<int>(); 
    // Start is called before the first frame update
    void Start()
    {
        healthHandler = GetComponent<HealthHandler>();
        enemy = GetComponent<EnemyMovement>();
    }

    public void StartPoison(int ticks)
    {
        if(tickTimer.Count <= 0) 
        {
            tickTimer.Add(ticks);
            StartCoroutine(Poison());
        }
        else
        {
            tickTimer.Add(ticks);
        }
    }

    IEnumerator Poison()
    {
        while(tickTimer.Count > 0)
        {
            for(int i = 0; i < tickTimer.Count; i++)
            {
                tickTimer[i]--;
            }
            enemy.e_IsHit.Invoke();
            healthHandler.TakeDamage(16);
            tickTimer.RemoveAll(i  => i == 0);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
