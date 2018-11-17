using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovementController : MonoBehaviour {

    // Public - movement variables
    public float speed;

    // Public - Squad references
    public SquadOffsets squadOffsets;

    public List<Character> members = new List<Character>();
    // Private - Squad offset controller
    
    // Movement buffer allowance
    public float movementBuffer;

    // Distance to swarm members before assigning to members
    float radius = 10;

    // Use this for initialization
    void Start ()
    {
		
	}

    public void playerDied(Character c)
    {
        members.Remove(c);
    }
 
    private void FixedUpdate()
    {
        // Test shooting
        Collider2D[] Colliders;
        int i = 0;
        for (i = 0; i < members.Count; i++)
        {
            if (members[i] == null)
            {
                continue;
            }

            if (members[i].getEnemyTargeted() == null)
            {
                Colliders = Physics2D.OverlapCircleAll(members[i].GetPosition(), radius);
                int j = 0;
                for (j = 0; j < Colliders.Length; j++)
                {
                    if (string.Compare(Colliders[j].gameObject.name, "Swarm Member(Clone)") == 0)
                    {
                        members[i].SetEnemyToTarget(Colliders[j].gameObject);
                    }
                }
            }

            Vector2 memberPositionDifference = members[i].GetPosition() - squadOffsets.GetMemberPosition(i);
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

    public List<Character> GetSquad()
    {
        List<Character> squadList = new List<Character>();
        for (int i = 0; i < 3; i++)
        {
            squadList.Add(members[i]);
        }
            return squadList;
    }
}
