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
    private AbilityUI uiAbility;

    private int squadSize;

    // Key to change player
    // TODO Talk to Rob about player changing
    // public KeyCode changePlayer;

    private int playerIndex;

	// Use this for initialization
	void Start ()
    {
        playerIndex = 0;
        uiAbility.SetAbility(playerIndex);
        offsets.SetArrangement(SquadOffsets.Arrangement.DIAMOND);
    }

    public void changePlayer(int newPlayerIndex)
    {
        Character playerReference = playerController.player;
        playerController.player = squadController.members[playerIndex];
        squadController.members[playerIndex] = playerReference;
        offsets.player = playerController.player;
        playerIndex = (newPlayerIndex) %  squadController.members.Count;
        uiAbility.SetAbility(playerIndex);
    }

    private void FixedUpdate()
    {
       
       
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            changePlayer(playerIndex + 1);
        }
 

        if (Input.GetMouseButtonDown(1))
        {
            playerController.player.UseAbility(GetComponent<Collider2D>(), playerController.player.transform.position, crosshairs.transform.position);
        }

        if (Input.GetButton("Fire1"))
        {
           playerController.player.attack(crosshairs.transform.position);
        }

    }

}
