using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletProjectile : MonoBehaviour
{

    public Rigidbody2D rb;
    
    private float speed  = 10;
    public float damage = 1;
    public float range = 1;

    private Vector3 startPosition;
    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        // If max distance has been traveled since creation, delete object
        if ((startPosition - transform.position).magnitude > range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.takeDamage(damage);
        }

        Destroy(gameObject);
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
        rb.velocity = transform.right * speed;
    }

    public void UpdateVariables(float speed, float damage, float range)
    {
        setSpeed(speed);
        this.damage = damage;
        this.range = range;
    } 
}
