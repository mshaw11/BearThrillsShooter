using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;


public class BulletProjectile : MonoBehaviour
{

    public Rigidbody2D rigidBody;
    

    private float speed  = 10;
    private float damage = 1;
    public float range = 1;
    private DamageType damageType = DamageType.PHYSICAL;
    private Vector3 startPosition;
    
    // Use this for initialization
    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        if (rigidBody == null)
        {
            throw new System.Exception("Game object does not have Rigidbody2D component");
        }
        startPosition = transform.position;
        rigidBody.velocity = transform.right * speed;
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
            enemy.takeDamage(damage, damageType);
        }

        Destroy(gameObject);
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
        this.rigidBody.velocity = transform.right * speed;
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void setRange(float range)
    {
        this.range = range;
    }

    public void UpdateVariables(float speed, float damage, float range)
    {
        setSpeed(speed);
        setDamage(damage);
        setRange(range);
    } 

}
