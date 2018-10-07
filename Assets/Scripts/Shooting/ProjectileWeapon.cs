using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
public class ProjectileWeapon : MonoBehaviour
{

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float fireRate = 10;
    [SerializeField]
    private float damage = 1;
    [SerializeField]
    private float range = 10;
    [SerializeField]
    private float speed = 20;
    [SerializeField]
    private bool playerControlled = true;

    [SerializeField]
    private DamageType damageType = DamageType.PHYSICAL;
 
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
        BulletProjectile bulletProjectile = Instantiate(bullet, firePoint, transform.rotation).GetComponent<BulletProjectile>();
        bulletProjectile.UpdateVariables(speed, damage, range);
    }
}
