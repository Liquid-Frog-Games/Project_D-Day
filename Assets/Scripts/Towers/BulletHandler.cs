using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    private float bulletDmg;

    private Transform target;

    public void SetTarget(Transform _target, float _dmg)
    {
        target = _target;
        bulletDmg = _dmg;
    }

    private void Start()
    {
        if (!target) return;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, bulletSpeed * Time.deltaTime);

        Vector2 direction = target.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        rb.velocity = (transform.right * bulletSpeed);  
        //Destroy bullet if there is no target

        if (!target)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        animator.SetTrigger("Hit");
		if(other.gameObject.layer == 8) return;

        other.gameObject.GetComponent<EnemyMovement>().e_IsHit.Invoke();
        other.gameObject.GetComponent<HealthHandler>().TakeDamage(bulletDmg);
        Destroy(gameObject);
    }
}
