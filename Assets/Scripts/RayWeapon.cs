using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayWeapon : MonoBehaviour
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
    public float damage = 100;
    public float range = 10;
    public float speed = 20;
    public bool playerControlled = true;
    
    [SerializeField]
    private DamageType damageType = DamageType.PHYSICAL;
 
    private float timeToFire = 0;
    private bool projectileWeapon = false;


    // Update is called once per frame
    void Update()
    {
        if (playerControlled)
        {
            //Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Shoot(transform.right);
        }
        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }


    void Shoot(Vector2 targetPosition)
    {
        // If the fire button is held down and it is within fire rate
        if (Input.GetButton("Fire1") && Time.time > timeToFire)
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
                bulletRay.range = hit.distance;
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage);
                }
            }

        }

    }

    void CreateBullet(Vector2 firePoint)
    {
        Instantiate(bullet, firePoint, transform.rotation);

        BulletRay bulletRay = bullet.GetComponent<BulletRay>();
        bulletRay.speed = speed;
        bulletRay.range = range;

    }
}
