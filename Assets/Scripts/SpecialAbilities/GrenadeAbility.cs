using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GrenadeAbility : AbilityBase
{
    [SerializeField]
    private GameObject grenade;
    [SerializeField]
    private float throwForce = 50f;
    void Start()
    {
        currentAbility = AbilityName.GRENADE;
    }

    protected override void ability(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition)
    {
        GameObject createdGrenade = Instantiate(grenade, playerPosition, Quaternion.identity);
        Rigidbody2D rb = createdGrenade.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(createdGrenade.GetComponent<Collider2D>(), playerCollider);
        rb.AddForce((crosshairPosition - playerPosition) * throwForce);
    }
}

