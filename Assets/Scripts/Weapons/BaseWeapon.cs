using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public abstract class BaseWeapon : MonoBehaviour
{

    [SerializeField]
    protected float damage = 1;
    [SerializeField]
    protected float range = 10;

    [SerializeField]
    protected DamageType damageType = DamageType.PHYSICAL;

    public abstract void attack(Vector2 targetPosition);
}
