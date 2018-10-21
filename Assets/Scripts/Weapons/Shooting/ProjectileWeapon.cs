using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class ProjectileWeapon : BaseWeapon
{

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float fireRate = 10;
    [SerializeField]
    private float speed = 20;
    [SerializeField]
    private bool playerControlled = true;


    private float timeToFire = 0;
    private bool projectileWeapon = false;

    private void Start()
    {
        if (bullet == null)
        {
            throw new System.Exception("Game object does not have Bullet specified");
        }
    }

    // Update is called once per frame
  
    private void Shoot()
    {
        // If the fire button is held down and it is within fire rate
       
        if (Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;

            Vector2 firePoint = new Vector2(transform.position.x, transform.position.y);
            // Get mouse position in world co-ordinates
            CreateBullet(firePoint);
        }
        
    }

    void CreateBullet(Vector2 firePoint)
    {
        BulletProjectile bulletProjectile = Instantiate(bullet, firePoint, transform.rotation).GetComponent<BulletProjectile>();
        bulletProjectile.UpdateVariables(speed, damage, range);
    }

    public override void attack(Vector2 targetPosition)
    {
        Shoot();
    }
}
