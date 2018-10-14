using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script attached to an empty object in the game world.

 * This script is responsible for controlling the movement of 
 * the player character. 
 *     - This can be changed through the SetPlayer() method which
 *       takes in a reference to a Member object.
 *     - Player and Squad Members both contain a Member Script 
 *       used to control their movement.
 */
public class PlayerMovementController : MonoBehaviour {

    // Public - Player reference
    public Member player;

    // Public - movement variables
    public float speedMultiplier;
    public float sprintMultiplier;
    public float jumpMultiplier;
    public float jumpCooldown;

    // Public - key codes
    public KeyCode dodgeCode;
    public KeyCode sprintCode;

    // Private - time to next dodge / current rotation
    private float nextDodgeTime;
    private Vector2 playerDirection;

    // Record time moving vertically / horizontally
    private float timeMovingRight;
    private float timeMovingLeft;
    private float timeMovingUp;
    private float timeMovingDown;

    // Use this for initialization
    void Start ()
    {
        if (player == null)
        {
            Debug.Log("Player reference is null in PlayerMovementController");
        }
	}

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void SetPlayer(Member member)
    {
        player = member;
    }

    public Member GetPlayer()
    {
        return player;
    }

    // Control Player based on input
    private void FixedUpdate()
    {
        SetPlayerRotation();
        SetPlayerVelocity();
    }

    private void SetPlayerRotation()
    {
        Vector2 playerPosition = player.GetPosition();
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
 
        playerDirection.Set(cameraPosition.x - playerPosition.x, 
                            cameraPosition.y - playerPosition.y);

        // E.g. tan(angle) = opposite/adjacent
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

        player.SetRotation(angle);
    }

    private void SetPlayerVelocity()
    {
        float horizontal = Input.GetAxis("Horizontal") * speedMultiplier;
        float vertical = Input.GetAxis("Vertical") * speedMultiplier;

        /* For arugment 1 (Force): 
         *  - Either use direction vector to dodge towards mouse position 
         *    or 
         *  - horizontal / vertical values to dodge in direction of movement (feels more natural)
         */
        if (Input.GetKeyDown(dodgeCode) && Time.time > nextDodgeTime)
        {
            nextDodgeTime = Time.time + jumpCooldown;
            player.SetForceAtPlayerPosition(new Vector2(horizontal, vertical) * jumpMultiplier);
        }
        else
        {
            if (Input.GetKey(sprintCode))
            {
                horizontal *= sprintMultiplier;
                vertical *= sprintMultiplier;
            }

            player.SetVelocity(horizontal, vertical);
        }
    }
}
