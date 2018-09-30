using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour {

    private float throwForce = 50f;

    [SerializeField]
    private GameObject Grenade;

    [SerializeField]
    private GameObject AreaOfEffect;

    public void UseAbility(int i, Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition)
    {
        switch (i)
        {
            case 0:
                ThrowGrenade(playerCollider, playerPosition, crosshairPosition);
                break;
            case 1:
                AOE(crosshairPosition);
                break;
        }
    }

    void ThrowGrenade(Collider2D playerCollider, Vector2 player, Vector2 crosshairs)
    {
        GameObject grenade = Instantiate(Grenade, player, Quaternion.identity);
        Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(grenade.GetComponent<Collider2D>(), playerCollider);
        rb.AddForce((crosshairs - player) * throwForce);
    }

    void AOE(Vector3 crosshair)
    {
        Instantiate(AreaOfEffect, crosshair, Quaternion.identity);
        AreaOfEffect.GetComponent<Rigidbody2D>();
    }

}

