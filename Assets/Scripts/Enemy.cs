using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float health = 100;
    private DamageType weakness = DamageType.PHYSICAL; 

    public void takeDamage(float damage, DamageType damageType)
    {

        float damageMultiplier = 1.0f;
        if (damageType.Equals(weakness))
        {
            damageMultiplier += 0.5f;
        }
        health = health - (damage * damageMultiplier);

        if (health < 0)
        {
            Die();
        }
    }

    private void Die ()
    {
        Destroy(gameObject);
    }
  
}
