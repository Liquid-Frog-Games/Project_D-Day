using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    public Animator anim;
    public HealthHandler health;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    //Pathfinding
    private Transform target;
    private int pathIndex = 0;
    private int availablePaths;
    private int chosenPathInt;
    private Transform[] chosenPath;

    //Animations
    private bool isInterrupted = false;
    private bool isDead = false;
    private bool isAttacking = false;
    private float previousVelocityX;
    private float previousVelocityY;

    [Header("Events")]
    public UnityEvent e_IsHit = new UnityEvent();
    public UnityEvent e_IsDead = new UnityEvent();

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

    public void Awake()
    {
        e_IsHit.AddListener(StartIsHit);
        e_IsDead.AddListener(StartIsDead);
    }

    private void Update()
    {
        if (!isDead)
        {
            //check if waypoint is reached, if so go to next point
            if(Vector2.Distance(target.position, transform.position) <= 0.1f ){
                pathIndex++;

                //Did we reach the endpoint?
                if (pathIndex == chosenPath.Length){

                    if (target.gameObject.name == "AttackPoint")
                    {
                        StartCoroutine(IsAttacking());
                    }
                    else
                    {
                        //Do damage
                        LevelManager.main.lives -= health.dmg;

                        //Destroy enemy
                        EnemySpawner.onEnemyDestroy.Invoke();
                        Destroy(gameObject);
                    }

                    //Did the player die?
                    if (LevelManager.main.lives <= 0f)
                    {
                        //Initiate game over scripts
                        LevelManager.main.lives = 0f;
                        LevelManager.e_GameOver.Invoke();
                    }

                    return;
                }	else {
                    if (!isAttacking)
                    {
                        //set the target waypoint to the next one in the array
                        target = chosenPath[pathIndex];
                    }
                }
            }
        }
        else
        {
            target.position = transform.position;
        }

        //Set the velocity of the previous frame if not interrupted
        if (!isInterrupted)
        {
            SetPreviousVelocity();
        }

        //Apply directional animations
        anim.SetFloat("Horizontal", previousVelocityX);
        anim.SetFloat("Vertical", previousVelocityY);
    }

    private void FixedUpdate() {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    private void SetPreviousVelocity()
    {
        previousVelocityX = rb.velocity.x;
        previousVelocityY = rb.velocity.y;
    }

    private void StartIsHit()
    {
        StartCoroutine(IsHit());
    }

    private void StartIsDead()
    {
        StartCoroutine(IsDead());
    }

    //GetHit
    public IEnumerator IsHit()
    {
        //Freeze enemy
        isInterrupted = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //Trigger animation and wait till its done
        anim.SetTrigger("GetHit");
        yield return new WaitForSeconds(0.5f);

        //Unfreeze enemy
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isInterrupted = false;
    }

    //IsDead
    private IEnumerator IsDead()
    {
        //Freeze enemy
        isDead = true;
        isInterrupted = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //Start animation and destroy
        anim.SetBool("Death", true);
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }

    //IsAttacking
    private IEnumerator IsAttacking()
    {
        isAttacking = true;

        //Freeze enemy
        isInterrupted = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        while (LevelManager.main.lives > 0f)
        {
            //Play animation
            anim.SetBool("Attack", true);

            //Do damage
            LevelManager.main.lives -= health.dmg;
            yield return new WaitForSeconds(1f);
        }

        if (LevelManager.main.lives <= 0f)
        {
            //Initiate game over scripts
            LevelManager.main.lives = 0f;
            LevelManager.e_GameOver.Invoke();
        }
    }
}
