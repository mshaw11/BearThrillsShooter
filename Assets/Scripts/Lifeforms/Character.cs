using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class Character : BaseLifeform
{
    [SerializeField]
    private BaseWeapon weapon;

    [SerializeField]
    private String characterName;

    private Rigidbody2D rigidBody;

    // Set Enemy target to attack
    private GameObject enemyToTarget;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    protected override void die()
    {
        Destroy(gameObject);
    }

    public void attack(Vector2 targetPosition)
    {
        if (weapon != null)
        {
            weapon.attack(targetPosition);
        }
    }

    public void attackEnemy()
    {
        if (enemyToTarget != null)
        {
            weapon.attack(enemyToTarget.transform.position);
        }
    }

    // ----------------- Movement of character -----------------------//

    public enum DirectionOfMovement
    {
        LEFT = 0,
        RIGHT = 1,
        UP = 2,
        DOWN = 3,
        NONE = 4
    }

    Rigidbody2D GetRigidBody()
    {
        return rigidBody;
    }

    public Vector2 GetPosition()
    {
        return rigidBody.position;
    }

    public float GetRotation()
    {
        return rigidBody.rotation;
    }

    public DirectionOfMovement GetDirectionOfMovement()
    {
        if (rigidBody.velocity.y > 0)
        {
            return DirectionOfMovement.UP;

        }
        else if (rigidBody.velocity.y < 0)
        {
            return DirectionOfMovement.DOWN;

        }
        else if (rigidBody.velocity.x > 0)
        {
            return DirectionOfMovement.RIGHT;

        }
        else if (rigidBody.velocity.x < 0)
        {
            return DirectionOfMovement.LEFT;

        }
        else
        {
            return DirectionOfMovement.NONE;
        }
    }

    public GameObject getEnemyTargeted()
    {
        return enemyToTarget;
    }

    public void SetVelocity(float horizontal, float vertical)
    {
        rigidBody.velocity = new Vector2(horizontal, vertical);
    }

    public void SetHorizontalVelocity(float value)
    {
        rigidBody.velocity = new Vector2(value, rigidBody.velocity.y);
    }

    public void SetVerticalVelocity(float value)
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, value);
    }

    public void SetRotation(float value)
    {
        rigidBody.rotation = value;
    }

    public void SetForceAtPlayerPosition(Vector2 force)
    {
        rigidBody.AddForceAtPosition(force, rigidBody.position);
    }

    public void SetEnemyToTarget(GameObject enemy)
    {
        enemyToTarget = enemy;
    }

    // ----------------- Movement of character -----------------------//
    private void FixedUpdate()
    {
        if (enemyToTarget != null)
        {
            Vector2 direction = new Vector2();
            Vector3 enemyPosition = enemyToTarget.transform.position;
        
            direction.Set(enemyPosition.x - transform.position.x,
                          enemyPosition.y - transform.position.y);

            // what is the distance to the enemy squred?
            float radius;
            radius = (direction.x * direction.x) + (direction.y * direction.y);

            if (radius < 100)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                SetRotation(angle);
                attackEnemy();
            }else
            {
                // Reset enemy reference
                enemyToTarget = null;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
