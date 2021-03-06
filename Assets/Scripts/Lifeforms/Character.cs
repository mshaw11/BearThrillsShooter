﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using System;

public class Character : BaseLifeform
{
    [SerializeField]
    private BaseWeapon weapon;

    [SerializeField]
    private String characterName;

    [SerializeField]
    private AbilityBase ability;

    [SerializeField]
    private Slider healthBar;

    private Rigidbody2D rigidBody;

    // Set Enemy target to attack
    private GameObject enemyToTarget;

    // Set Player to follow when not attacking enemy
    private Character player;
    
    public bool isControlled = false;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        if (ability != null)
        {
            ability = Instantiate(ability);
        }
       
        if (healthBar != null)
        {
            healthBar.GetComponentInChildren<Text>().text = (characterName);
        }
    }

    protected override void die()
    {
        MovementManager manager = (MovementManager)GameObject.FindGameObjectWithTag("movementManager").GetComponent<MovementManager>();

        if (isControlled)
        {
            manager.playerDied();
        }

        manager.squadController.playerDied(this);


        if (healthBar != null)
        {
            healthBar.value = 0;
        }
        Destroy(gameObject);
    }
   
    public void attack(Vector2 targetPosition)
    {
        if (weapon != null)
        {
            Debug.Log("Weapon attacking");
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
    
    public override void  takeDamage(float damage, DamageType damageType)
    {
        base.takeDamage(damage, damageType);

        // Update health bar info
        if (healthBar != null)
        {
            healthBar.value = health / maxHealth;
        }

    }

    // ---------------- Usign abilities ----------------------------//
    public void UseAbility(Collider2D playerCollider, Vector3 playerPosition, Vector3 crosshairPosition)
    {
        if (ability != null)
        {
            ability.useAbility(playerCollider, playerPosition, crosshairPosition);
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

    public AbilityBase GetAbility()
    {
        return ability;
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

    public void SetPlayer(Character playerToTarget)
    {
        player = playerToTarget;
    }

    public void RotateTowards(Vector2 position)
    {
        Vector2 direction = new Vector2();
    
        direction.Set(position.x - transform.position.x,
                      position.y - transform.position.y);


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        SetRotation(angle);
    }

    // ----------------- Movement of character -----------------------//
    private void FixedUpdate()
    {
        if (!isControlled)
        {
            if (enemyToTarget != null)
            {
                Vector2 direction = new Vector2();
                Vector3 enemyPosition = enemyToTarget.transform.position;
            
                direction.Set(enemyPosition.x - transform.position.x,
                              enemyPosition.y - transform.position.y);

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // what is the distance to the enemy squred?
                float radius;
                radius = (direction.x * direction.x) + (direction.y * direction.y);

                if (radius < 100)
                {
                    SetRotation(angle);
                    attackEnemy();
                }else
                {
                    // Reset enemy reference
                    enemyToTarget = null;
                }
            } else
            {
                RotateTowards(player.GetPosition());
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
