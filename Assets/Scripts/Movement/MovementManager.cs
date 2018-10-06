using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    // Access to player and squad controllers
    public PlayerMovementController playerController;
    public SquadMovementController squadController;
    public SquadOffsets offsets;

    // Key to change player
    public KeyCode changePlayer;

    private int playerIndex;

	// Use this for initialization
	void Start ()
    {
        playerIndex = 0;
        offsets.SetArrangement(SquadOffsets.Arrangement.DIAMOND);
	}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(changePlayer))
        {
            Member playerReference = playerController.player;
            playerController.player = squadController.members[playerIndex];
            squadController.members[playerIndex] = playerReference;
            offsets.player = playerController.player;
            playerIndex = (playerIndex += 1) % 3;
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
