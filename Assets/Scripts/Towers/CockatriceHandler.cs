using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CockatriceHandler : MonoBehaviour
{
    [Header("Attributes")]
    public Animator animator;
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f;        // Bullet per second

    public bool bought = false;
    private bool hasShot = false;

    [Header("References")]
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

            RotateTowardsTarget();
            if (!CheckTargetIsInRange())
            {
                target = null;
            }
            else
            {
                timeUntilFire += Time.deltaTime;

                if (timeUntilFire >= 1f / bps)
                {
                    timeUntilFire = 1f / bps;

                    if (timeUntilFire == 1f / bps)
                    {
                        StartCoroutine(prepShot());
                    }
                }
            }
        }
        return;

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
        PoisonHandler bulletScript = bulletObj.GetComponent<PoisonHandler>();
        bulletScript.SetTarget(target);         //target and damage amount have to be passed
    }

    private IEnumerator prepShot()
    {
        hasShot = false;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.9f);
        timeUntilFire = 0f;
        if (!hasShot)
        {
            hasShot = true;
            Shoot();
        }
    }
}
