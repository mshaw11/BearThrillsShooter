using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameRingAbility : AbilityBase
{
    [SerializeField]
    private GameObject flameRing;

    public override void useAbility(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition)
    {
        Instantiate(flameRing, crosshairPosition, Quaternion.identity);
    }
}