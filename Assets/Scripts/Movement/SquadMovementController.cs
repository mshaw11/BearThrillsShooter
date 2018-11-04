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
        int i = 0;
        for (i = 0; i < members.Count; i++)
        {
            if (members[i] == null)
            {
                continue;
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
}
