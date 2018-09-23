using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour {


    public float radius = 5f;
    public float force = 700f;
    public float delay = 0f;


    Camera viewCamera;
    public enum EffectType // your custom enumeration
    {
        Instant,
        Projectile
    };
    public AreaOfEffect.EffectType aoeType = EffectType.Instant;

    public ParticleSystem ps;
    public GameObject explosionAnimation;

    float countdown;
    bool hasExploded = false;
    // Use this for initialization
    void Start () {
        countdown = delay;
        viewCamera = Camera.main;
    }
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    public virtual void Explode()
    {
        if (aoeType == EffectType.Instant)
        {
            Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.back, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector2 point = ray.GetPoint(rayDistance);
                transform.position = point;
                Instantiate(explosionAnimation, transform.position, transform.rotation);

                //Get nearby enemies
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

                foreach (Collider2D nearbyObj in colliders)
                {
                    // Add force
                    Rigidbody2D rb = nearbyObj.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(force, transform.position, radius);
                    }
                }
            }
            //Explosion effect
            
           
        }
        else
        {
            //Explosion effect
            Instantiate(explosionAnimation, transform.position, transform.rotation);

        }

        Destroy(gameObject);
        
    }

}
