using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{

   private enum DamageType
    {
        WATER,
        FIRE,
        RADIATION,
        PHYSICAL
    }

    public LayerMask whatToHit;
    public GameObject bullet;
    public float fireRate = 10;
    public float damage = 1;
    public float range = 10;
    public float speed = 20;
    public bool playerControlled = true;
    
    [SerializeField]
    private DamageType damageType = DamageType.PHYSICAL;
 
    private float timeToFire = 0;
    private bool projectileWeapon = false;

    LineRenderer lr;

    // Use this for initialization

    
    // Update is called once per frame
    void Update()
    {
        if (playerControlled)
        {
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        
    }


    void Shoot(Vector2 targetPosition)
    {
        // If the fire button is held down and it is within fire rate
        if (Input.GetButton("Fire1") && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;

            Vector2 firePoint = new Vector2(transform.position.x, transform.position.y);
            // Get mouse position in world co-ordinates
            CreateBullet(firePoint);
        }

    }

    void CreateBullet(Vector2 firePoint)
    {
        Instantiate(bullet, firePoint, transform.rotation);

        BulletProjectile bulletProjectile = bullet.GetComponent<BulletProjectile>();
        
        bulletProjectile.UpdateVariables(speed, damage, range);
        
    }
}
