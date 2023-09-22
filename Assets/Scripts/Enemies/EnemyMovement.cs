using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")] 
	[SerializeField] private Rigidbody2D rb;
	public HealthHandler health;
   
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private int availablePaths;
    private int chosenPathInt;
    private Transform[] chosenPath;

    private void Start()
    {
        //Get the length of pathChoises for all the possible paths
        availablePaths = LevelManager.main.pathChoices.Length;

		//Choose a random array of the possibilities
        chosenPathInt = Random.Range(0, availablePaths);

		//Get this choice and call it chosenPath
        chosenPath = LevelManager.main.pathChoices[chosenPathInt].waypoints;

        target = chosenPath[pathIndex];
    }
    
    private void Update()
    {
		//check if waypoint is reached, if so go to next point
        if(Vector2.Distance(target.position, transform.position) <= 0.1f ){
			pathIndex++;

			//Did we reach the endpoint?
			if (pathIndex == chosenPath.Length){

				//Destroy enemy and do damage
				EnemySpawner.onEnemyDestroy.Invoke();
				LevelManager.main.lives -= health.dmg;
				Destroy(gameObject);

				//Did the player die?
				if (LevelManager.main.lives <= 0f)
				{
					//Initiate game over scripts
					LevelManager.main.lives = 0f;
					LevelManager.e_GameOver.Invoke();
				}

				return;	
			}	else {
					//set the target waypoint to the next one in the array
             		target = chosenPath[pathIndex];
			}
		}
    }

    private void FixedUpdate() {
		Vector2 direction = (target.position - transform.position).normalized;

		rb.velocity = direction * moveSpeed;
	}

}
