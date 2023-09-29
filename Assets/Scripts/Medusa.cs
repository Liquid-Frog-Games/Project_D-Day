using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medusa : MonoBehaviour
{
    public static Medusa main;

    [Header("References")]
    public int soulCount;
    public int soulRequirement;
    public Slider soulBar;

    private void Start()
    {
        //Sync SoulReq to slider maxValue
        if (soulBar)
        {
            soulBar.maxValue = soulRequirement;
        }
    }

    private void Awake()
    {
        main = this;
    }

    //Ability button is pressed
    public void UseAbility()
    {
        //Check for soul count (equal or more than 100 souls, 1 per enemy)
        if (soulCount >= soulRequirement)
        {
            //Remove Required souls
            soulCount -= soulRequirement;

            //Get all active enemies on screen
            foreach (var item in LevelManager.main.enemyList)
            {
                //Apply the timed freeze to enemy
                StartCoroutine(Petrify(item));
            }
        }
    }

    private IEnumerator Petrify(GameObject enemy)
    {
        //Apply ability
        if (enemy.TryGetComponent<EnemyMovement>(out EnemyMovement em))
        {
            //Freeze enemy
            em.FreezeEnemy();
            em.isFrozen = true;
            //Stop walkin animation/use petrify animation
            em.anim.speed = 0;
            //Wait 5 seconds
            yield return new WaitForSeconds(5f);
            //Unfreeze enemy
            em.UnFreezeEnemy();
            if (em.anim)
            {
                em.anim.speed = 1;
            }
            em.isFrozen = false;
        }
    }

    public void AddSouls(int amount)
    {
        soulCount += amount;
    }

    //Sync SoulCount to slider value
    private void OnGUI()
    {
        if (soulBar)
        {
            soulBar.value = soulCount;
        }
    }

}
