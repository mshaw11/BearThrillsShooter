using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackAbility : MonoBehaviour {

    public PolygonCollider2D areaOfEffect;
    public float cooldown = 1.0f;
    public float strength = 4.0f;

    //public GameObject effect;

    private float timeToFire = 0;
	// Use this for initialization
	void Start () {
        
	}

    private void Update()
    {
        timeToFire += Time.deltaTime;

        Debug.Log("jeses " + timeToFire);
          if (Input.GetButton("Fire1") && timeToFire > cooldown)
        {
            timeToFire = 0;
            knockBack();
        }



    }

    void knockBack()
    {
       // var balls = Instantiate(effect, transform.parent.position,  transform.rotation, transform.parent);
      
        Collider2D[] colliders = new Collider2D[100];
        ContactFilter2D contactFilter = new ContactFilter2D();

        int test = areaOfEffect.OverlapCollider(contactFilter, colliders);


        Debug.Log(test);
        for (int i = 0; i <test; i++)
        {
            Enemy enemy = colliders[i].GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.knockBack(transform.position, strength);
            }
        }

        Debug.Log("Amount of collisions - " + test);
    }
}
