﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour {

    [SerializeField]
    private float radius = 5f;

    [SerializeField]
    private float force = 700f;

    [SerializeField]
    private float delay = 0f;

    [SerializeField]
    private int stunFrames = 200;


    Camera viewCamera;
    public enum EffectType
    {
        Instant,
        Projectile
    };
    public EffectType aoeType = EffectType.Instant;

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
        //If Instant detonation, apply force to objects within the radius
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

                    if (nearbyObj.gameObject.gameObject.layer == LayerMask.NameToLayer("Enemies"))
                    {
                        var swarmMember = nearbyObj.gameObject.GetComponent<SwarmMember>();
                        if (swarmMember != null)
                        {
                            swarmMember.HitByAbility(stunFrames);
                        }
                        Rigidbody2D rb = nearbyObj.GetComponent<Rigidbody2D>();
                        if (rb != null)
                        {
                            rb.AddExplosionForce(force, transform.position, radius);
                        }
                    }
                }
            }
        }
        //Else simply instantiate the explosion and let it apply the force from within that gameobject
        else
        {
            Instantiate(explosionAnimation, transform.position, transform.rotation);

        }

        Destroy(gameObject);
        
    }

}
