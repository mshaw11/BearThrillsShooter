using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is responsible for updaing the offets 
 * of squad members based on the players position / 
 * direction of travel. 
 * 
 * Used by SquadMovementController to set Squad member
 * velocities.
 * 
 */ 
public class SquadOffsets : MonoBehaviour {
    
    public enum Arrangement : byte
    {
        LINE = 0,
        DIAMOND = 1
    }

    private enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    private Arrangement arrangement;
    private Direction direction;

    // Player to base offsets of
    public Character player;

    // Keep current position of player
    private Vector2 playerPosition;

    // Record time moving vertically / horizontally
    private float timeMovingRight;
    private float timeMovingLeft;
    private float timeMovingUp;
    private float timeMovingDown;

    // Store where members should be based on player
    private Vector2 [] memberOffsets = new Vector2[3];

     // Store offsets based on direction of travel for line formation
    // index 0: Horizontal, 1: Vertical
    public Vector2 [] memberOneLineOffsets = new Vector2[2];
    public Vector2 [] memberTwoLineOffsets = new Vector2[2];
    public Vector2 [] memberThreeLineOffsets = new Vector2[2];

   // Store offsets based on direction of travel for diamond formation
    // index order: DOWN, UP, LEFT, RIGHT
    public Vector2 [] memberOneDiamondOffsets = new Vector2[4];
    public Vector2 [] memberTwoDiamondOffsets = new Vector2[4];
    public Vector2 [] memberThreeDiamondOffsets = new Vector2[4];

    // Store direction of movement
    private Character.DirectionOfMovement directionMoving;

    // Use this for initialization
    void Start ()
    {
        // Starting arrangement 
        arrangement = Arrangement.LINE;
        // Starting direction offset
        direction = Direction.UP;
	}

    private void FixedUpdate()
    {
        switch (player.GetDirectionOfMovement())
        {
            case Character.DirectionOfMovement.UP:
                Debug.Log("UP");
                timeMovingUp += Time.deltaTime;
                timeMovingDown = 0;
                timeMovingLeft = 0;
                timeMovingRight = 0;
                break;
            case Character.DirectionOfMovement.DOWN:
                timeMovingUp = 0;
                timeMovingDown += Time.deltaTime;
                timeMovingLeft = 0;
                timeMovingRight = 0;
                break;
            case Character.DirectionOfMovement.LEFT:
                timeMovingUp = 0;
                timeMovingDown = 0;
                timeMovingLeft += Time.deltaTime;
                timeMovingRight = 0;
               break;
            case Character.DirectionOfMovement.RIGHT:
                timeMovingUp = 0;
                timeMovingDown = 0;
                timeMovingLeft = 0;
                timeMovingRight += Time.deltaTime;
              break;
            case Character.DirectionOfMovement.NONE:
                timeMovingUp = 0;
                timeMovingDown = 0;
                timeMovingLeft = 0;
                timeMovingRight = 0;
             break;
        }

        if (timeMovingUp > 0.75f)
        {
            direction = Direction.UP;

        } else if (timeMovingDown > 0.75f)
        {
            direction = Direction.DOWN;

        } else if (timeMovingLeft > 0.75f)
        {
            direction = Direction.LEFT;

        } else if (timeMovingRight > 0.75f)
        {
            direction = Direction.RIGHT;
        }

        playerPosition = player.GetPosition();

        switch(arrangement)
        {
            case Arrangement.LINE:
                switch (direction)
                {
                    case Direction.DOWN:
                    case Direction.UP:
                        memberOffsets[0] = playerPosition + memberOneLineOffsets[1];
                        memberOffsets[1] = playerPosition + memberTwoLineOffsets[1];
                        memberOffsets[2] = playerPosition + memberThreeLineOffsets[1];
                       break;
                    case Direction.LEFT:
                    case Direction.RIGHT:
                        memberOffsets[0] = playerPosition + memberOneLineOffsets[0];
                        memberOffsets[1] = playerPosition + memberTwoLineOffsets[0];
                        memberOffsets[2] = playerPosition + memberThreeLineOffsets[0];
                        break;
                }
                break;
            case Arrangement.DIAMOND:
                switch (direction)
                {
                    case Direction.DOWN:
                        memberOffsets[0] = playerPosition + memberOneDiamondOffsets[0];
                        memberOffsets[1] = playerPosition + memberTwoDiamondOffsets[0];
                        memberOffsets[2] = playerPosition + memberThreeDiamondOffsets[0];
                        break;
                    case Direction.UP:
                        memberOffsets[0] = playerPosition + memberOneDiamondOffsets[1];
                        memberOffsets[1] = playerPosition + memberTwoDiamondOffsets[1];
                        memberOffsets[2] = playerPosition + memberThreeDiamondOffsets[1];
                       break;
                    case Direction.LEFT:
                        memberOffsets[0] = playerPosition + memberOneDiamondOffsets[2];
                        memberOffsets[1] = playerPosition + memberTwoDiamondOffsets[2];
                        memberOffsets[2] = playerPosition + memberThreeDiamondOffsets[2];
                        break;
                    case Direction.RIGHT:
                        memberOffsets[0] = playerPosition + memberOneDiamondOffsets[3];
                        memberOffsets[1] = playerPosition + memberTwoDiamondOffsets[3];
                        memberOffsets[2] = playerPosition + memberThreeDiamondOffsets[3];
                        break;
                }
                break;
        }
        
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public Vector2 GetMemberPosition(int member)
    {
        return memberOffsets[member];
    }

    public void SetArrangement(Arrangement arr)
    {
        arrangement = arr;
    }
}
