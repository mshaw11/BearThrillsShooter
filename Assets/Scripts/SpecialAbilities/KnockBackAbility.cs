using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackAbility : AbilityBase {
   
    private PolygonCollider2D areaOfEffect;

    [SerializeField]
    private float strength = 4.0f;

    // The size of the array used to check collisions within area of effect
    private int maxAmountOfColliders = 100;

    private Collider2D[] colliders;
    private ContactFilter2D contactFilter;
    // Use this for initialization
    void Start () {
       
        areaOfEffect = GameObject.FindWithTag("KnockBackCone").GetComponent<PolygonCollider2D>();
        if (areaOfEffect == null)
        {
            throw new System.Exception("Game object does not have line PolygonCollider2D component");
        }
        
        colliders = new Collider2D[maxAmountOfColliders];
        contactFilter = new ContactFilter2D();
      
    }

   
    protected override void ability(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition)
    {
       
        // Update the collision array. Returns amount of collisions
        int collisions = areaOfEffect.OverlapCollider(contactFilter, colliders);
        Debug.Log("coliisions = " + collisions);
        for (int i = 0; i < collisions; i++)
        {
            Enemy enemy = colliders[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                // Dynamic knock back
                Vector2 currentPositition = enemy.transform.position;
                Vector2 directionOfPush = gameObject.transform.position;
                Vector2 knockbackPosition = currentPositition + (currentPositition - directionOfPush).normalized * strength;
                enemy.GetComponent<Rigidbody2D>().AddForce(knockbackPosition, ForceMode2D.Impulse);
            }
        }
    }
}
