using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    // Access to player and squad controllers
    public PlayerMovementController playerController;
    public SquadMovementController squadController;
    public SquadOffsets offsets;


    [SerializeField]
    private Crosshairs crosshairs;

    [SerializeField]
    private AbilityHandler abilityHandler;

    // Key to change player
    public KeyCode changePlayer;

    private int playerIndex;

	// Use this for initialization
	void Start ()
    {
        playerIndex = 0;
        offsets.SetArrangement(SquadOffsets.Arrangement.DIAMOND);

        abilityHandler = Instantiate(abilityHandler);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(changePlayer))
        {
            Character playerReference = playerController.player;
            playerController.player = squadController.members[playerIndex];
            squadController.members[playerIndex] = playerReference;
            offsets.player = playerController.player;
            playerIndex = (playerIndex += 1) % 3;
        }

        if (Input.GetMouseButtonDown(1))
        {
            abilityHandler.UseAbility(GetComponent<Collider2D>(), playerController.player.transform.position, crosshairs.transform.position);
        }

        if (Input.GetButton("Fire1"))
        {
            playerController.player.attack(crosshairs.transform.position);
        }
    }
}
