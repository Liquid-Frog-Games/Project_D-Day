using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDmg = 1;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
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
        rb.velocity = (transform.right * bulletSpeed);  //it works BUT! its aim calculations are weird so gotta check that before merge.
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
		if(other.gameObject.layer == 8) return;

        other.gameObject.GetComponent<HealthHandler>().TakeDamage(bulletDmg);
        Destroy(gameObject);
    }
}
