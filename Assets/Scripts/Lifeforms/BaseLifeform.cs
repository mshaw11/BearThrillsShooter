using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public abstract class BaseLifeform : MonoBehaviour {

    [SerializeField]
    protected float health = 100;

    [SerializeField]
    protected float maxHealth = 100;
    private DamageType weakness = DamageType.PHYSICAL;
    private DamageType strength = DamageType.PHYSICAL;

    public virtual void takeDamage(float damage, DamageType damageType)
    {

        float damageMultiplier = 1.0f;
        if (damageType.Equals(weakness))
        {
            damageMultiplier += 0.5f;
        }
        health = health - (damage * damageMultiplier);

        if (health < 0)
        {
            die();
        }
    }

    protected abstract void die();
  
}
