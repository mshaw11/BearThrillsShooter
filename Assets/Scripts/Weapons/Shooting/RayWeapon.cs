using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class RayWeapon : BaseWeapon
{
    [SerializeField]
    private LayerMask whatToHit;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float fireRate = 10;
    [SerializeField]
    private float speed = 20;
    [SerializeField]
    private bool playerControlled = true;
 
    private float timeToFire = 0;

    private void Shoot()
    {
        Vector2 targetPosition = transform.right;
        // If the fire button is held down and it is within fire rate
        if (Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;

            Vector2 firePoint = new Vector2(transform.position.x, transform.position.y);
            // Cast a ray from the firing point to target
            RaycastHit2D hit = Physics2D.Raycast(firePoint, targetPosition, range, whatToHit);

            CreateBullet(firePoint);

            // We must do damage calculations here
            BulletRay bulletRay = bullet.GetComponent<BulletRay>();

           
            // True if ray has interrsected a hittable collider
            if (hit.collider != null)
            {
                // Set the ray max distance so that it does not pass further than the hit object
                bulletRay.setRange(hit.distance);
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage, damageType);
                }
            }

        }

    }

    void CreateBullet(Vector2 firePoint)
    {
        Instantiate(bullet, firePoint, transform.rotation);

        BulletRay bulletRay = bullet.GetComponent<BulletRay>();
        bulletRay.updateVariables(speed, range);
    }

    public override void attack(Vector2 targetPosition)
    {
        Shoot();
    }
}
