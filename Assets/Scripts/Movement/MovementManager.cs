using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;


public class MovementManager : MonoBehaviour {

    // Access to player and squad controllers
    public PlayerMovementController playerController;
    public SquadMovementController squadController;
    public SquadOffsets offsets;

    [SerializeField]
    private Crosshairs crosshairs;

    private int playerIndex;

    private AbilityUI uiAbility;

    // Key to change player
    // TODO Talk to Rob about player changing
    // public KeyCode changePlayer;

    private const int squadSize = 4;

	// Use this for initialization
	void Start ()
    {
        playerIndex = 0;
        offsets.SetArrangement(SquadOffsets.Arrangement.DIAMOND);
    }

    private void changePlayer(int newPlayerIndex)
    {
        Character playerReference = playerController.player;
        playerController.player = squadController.members[playerIndex];
        squadController.members[playerIndex] = playerReference;
        offsets.player = playerController.player;
        playerIndex = (newPlayerIndex) %  3;

        uiAbility.SetAbility(playerController.player.GetAbility().currentAbility);
    }
    private void FixedUpdate()
    {
        if (playerController.player.GetAbility() != null)
        {
            uiAbility.SetAbility(playerController.player.GetAbility().currentAbility);
        }
        
        offsets.SetArrangement(SquadOffsets.Arrangement.DIAMOND);
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && playerIndex < (squadSize - 1))
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

    public List<Character> GetSquadMembers()
    {
        var squadAndPlayer = new List<Character>();
        var squad = squadController.GetSquad();
        squadAndPlayer.Add(playerController.GetPlayer());
        foreach (var member in squad)
        {
            squadAndPlayer.Add(member);
        }
        return squadAndPlayer;
    }

}
