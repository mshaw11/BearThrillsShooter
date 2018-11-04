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
     
    // Use this for initialization
    void Start ()
    {
		
	}

    private void FixedUpdate()
    {
        int i = 0;
        for (i = 0; i < 3; i++)
        {
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
