using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class FlameRingAbility : AbilityBase
{
    [SerializeField]
    private GameObject flameRing;
    void Start()
    {
        currentAbility = AbilityName.FLAME;
    }

    protected override void ability(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition)
    {
        Instantiate(flameRing, crosshairPosition, Quaternion.identity);
    }
}