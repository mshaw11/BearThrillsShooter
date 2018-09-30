using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 100;

    public void takeDamage(float damage)
    {
        health = health - damage;

        if (health < 0)
        {
            Die();
        }
    }

    public void knockBack(Vector2 direction, float force)
    {
        // Dynamic knock back
        Vector2 currentPositition = transform.position;
        Vector2 knockbackPosition = currentPositition + (currentPositition - direction).normalized *force;
        GetComponent<Rigidbody2D>().AddForce(knockbackPosition, ForceMode2D.Impulse);
    }

    private void Die ()
    {
        Destroy(gameObject);
    }
  
}
