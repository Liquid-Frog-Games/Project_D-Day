using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HydraHandler : MonoBehaviour
{
    [Header("Attributes")]
    public Animator animator;
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f;        // Bullet per second

    public bool bought = false;

    [Header("References")]
    public AudioSource hydraRoar;
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject range;

    private Transform target;
    private float timeUntilFire;


    private void Start()
    {
        range.transform.localScale = new Vector3(targetingRange, targetingRange, 0);
    }

    void Update()
    {
        if (bought == true)
        {
            if (target == null)
            {
                FindTarget();
                return;
            }
            StartCoroutine(PlayRoar());
            RotateTowardsTarget();
            if (!CheckTargetIsInRange())
            {
                target = null;
            }
            else
            {
                timeUntilFire += Time.deltaTime;
                animator.SetTrigger("Attack");
                if (timeUntilFire >= 1f / bps)
                {
                    
                    Shoot();
                    Invoke("Shoot", 0.2f);
                    timeUntilFire = 0f;
                }
            }
        }
    }

    //Enable the range circle in Unity editor, NOTE: THIS MUST BE DISABLED FOR BUILDS, IT WILL CRASH OTHERWISE
#if UNITY_Editor
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
#endif

    public void ToggleActive()
    {
        bought = true;

        Destroy(range);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position,
        0f, enemyMask);
      
        if (hits.Length > 0)
            {
                target = hits[0].transform;
            }
        
       
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) *
                      Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        BulletHandler bulletScript = bulletObj.GetComponent<BulletHandler>();
        bulletScript.SetTarget(target, 27.5f);    //target and damage amount have to be passed
    }

    private IEnumerator PlayRoar()
    {
        int randomInt = Random.Range(0, 10);
        if (randomInt == 9)
        {
           hydraRoar.Play();
        }
        yield return new WaitForSeconds(30f);
    }
}
