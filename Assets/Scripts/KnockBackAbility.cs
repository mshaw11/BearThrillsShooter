using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackAbility : MonoBehaviour {

    private PolygonCollider2D areaOfEffect;

    [SerializeField]
    private float cooldown = 1.0f;
    [SerializeField]
    private float strength = 4.0f;

    // The size of the array used to check collisions within area of effect
    private int maxAmountOfColliders = 100;

    private float timeToFire = 0;
    private Collider2D[] colliders;
    private ContactFilter2D contactFilter;
    // Use this for initialization
    void Start () {
        areaOfEffect = GetComponent<PolygonCollider2D>();
        if (areaOfEffect == null)
        {
            throw new System.Exception("Game object does not have line PolygonCollider2D component");
        }

        colliders = new Collider2D[maxAmountOfColliders];
        contactFilter = new ContactFilter2D();
    }

    private void Update()
    {
        timeToFire += Time.deltaTime;

        if (Input.GetButton("Fire1") && timeToFire > cooldown)
        {
            timeToFire = 0;
            knockBack();
        }
    }

    public void knockBack()
    {
       // Update the collision array. Returns amount of collisions
        int collisions = areaOfEffect.OverlapCollider(contactFilter, colliders);

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
