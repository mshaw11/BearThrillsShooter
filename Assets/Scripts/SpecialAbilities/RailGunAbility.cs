using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class RailGunAbility : AbilityBase {
   
    [SerializeField]
    private float damage = 2000;
    [SerializeField]
    private float range = 10;
    [SerializeField]
    private LayerMask whatToHit;

    private LineRenderer lineRenderer;
    private DamageType damageType = DamageType.RADIATION;

    public void Start()
    {

        this.lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            throw new System.Exception("Game object does not have lineRenderer component");
        }
    }

    protected override void ability(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition)
    {
        Vector3 direction = crosshairPosition - playerPosition;

        // Cast a ray from the firing point to target
        RaycastHit2D[] hits = Physics2D.RaycastAll(playerPosition, direction, range, whatToHit);

        lineRenderer.SetPosition(0, playerPosition);
        lineRenderer.SetPosition(1, direction*range + playerPosition);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                // Set the ray max distance so that it does not pass further than the hit object
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage, damageType);
                }
            }
        }

        StartCoroutine(drawLine());
    }

    private IEnumerator drawLine ()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.02f);
        lineRenderer.enabled = false;
    }
}
