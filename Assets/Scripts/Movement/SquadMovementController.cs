using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovementController : MonoBehaviour {

    // Public - movement variables
    public float speed;

    // Public - Squad references
    public Character[] members = new Character[3];

    // Private - Squad offset controller
    public SquadOffsets squadOffsets;

    // Movement buffer allowance
    public float movementBuffer;

    // radius to shoot
    public float radius = 10;

    // Use this for initialization
    void Start ()
    {
		
	}

    private void FixedUpdate()
    {
        // Test shooting
        Collider2D[] Colliders;
        Colliders = Physics2D.OverlapCircleAll(members[1].GetPosition(), radius);
        int i = 0;
        for (i = 0; i < 3; i++)
        {
            Vector2 memberPosition = members[i].GetPosition();

            if (string.Compare(Colliders[i].gameObject.name, "Swarm Member") == 1)
            {
                //Vector2 direction = new Vector2();
                //Vector3 enemyPosition;

                //    if (members[i].getEnemyTargeted() != null)
                //    {
                //        enemyPosition = members[i].getEnemyTargeted().transform.position;
                //    }
                //    else
                //    {
                //        members[i].SetEnemyToTarget(Colliders[i].gameObject);
                //        enemyPosition = Colliders[i].transform.position;
                //    }

                //    direction.Set(enemyPosition.x - memberPosition.x,
                //                        enemyPosition.y - memberPosition.y);

                //    // E.g. tan(angle) = opposite/adjacent
                //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                //    if (i < 3)
                //    {
                //        members[i].SetRotation(angle);
                //        members[i].attack(Colliders[i].transform.position);
                //    }
                //    else
                //    {
                //        break;
                //    }
            }

            Vector2 memberPositionDifference = memberPosition - squadOffsets.GetMemberPosition(i);
            float memberXDifference = memberPositionDifference.x;
            float memberYDifference = memberPositionDifference.y;

            // Horizontal velocity
            if (Mathf.Abs(memberXDifference) > movementBuffer)
            {
                if (memberXDifference > 0)
                {
                    members[i].SetHorizontalVelocity(-speed);

                }
                else
                {
                    members[i].SetHorizontalVelocity(speed);
                }
            }
            else
            {
                members[i].SetHorizontalVelocity(0);
            }

            // Vertical velocity
            if (Mathf.Abs(memberYDifference) > movementBuffer)
            {
                if (memberYDifference > 0)
                {
                    members[i].SetVerticalVelocity(-speed);

                }
                else
                {
                    members[i].SetVerticalVelocity(speed);
                }
            }
            else
            {
                members[i].SetVerticalVelocity(0);
            }
        }
    }


    // Update is called once per frame
    void Update ()
    {
		
	}
}
