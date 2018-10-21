﻿using System.Collections;
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
}