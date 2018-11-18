using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;


public class MovementManager : MonoBehaviour {

    // Access to player and squad controllers
    public PlayerMovementController playerController;
    public SquadMovementController squadController;
    public SquadOffsets offsets;

    [SerializeField]
    private Crosshairs crosshairs;

    private int playerIndex;

    [SerializeField]
    private AbilityUI uiAbility;

    private int squadSize;

    // Use this for initialization
    void Start ()
    {
        playerController.player.isControlled = true;
        playerIndex = 0;
        offsets.SetArrangement(SquadOffsets.Arrangement.DIAMOND);

        for (int i = 0; i < squadController.members.Count; i++)
        {
            squadController.members[i].SetPlayer(playerController.player);
        }
    }

    public void playerDied()
    {
        if (squadController.members.Count == 0)
        {
            // GAME OVER
            GameObject.FindGameObjectWithTag("gameOverText").GetComponent<Text>().enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            changePlayer(0);
        }
    }

    public void changePlayer(int newPlayerIndex)
    {
        int memberCount = squadController.members.Count;
        if (playerIndex >= memberCount)
        {
            playerIndex = 0;
        }
        if (playerIndex <= memberCount && memberCount != 0)
        {
            Character playerReference = playerController.player;
            playerReference.isControlled = false;
            playerController.player = squadController.members[playerIndex];
            squadController.members[playerIndex] = playerReference;
            offsets.player = playerController.player;

            playerIndex = (newPlayerIndex) % squadController.members.Count;
            playerController.player.isControlled = true;
            playerController.player.SetEnemyToTarget(null);

            for (int i = 0; i < memberCount; i++)
            {
                squadController.members[i].SetPlayer(playerController.player);
            }

            if (uiAbility != null)
            {
                uiAbility.SetAbility(playerController.player.GetAbility().currentAbility);
            }
        }
       
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            changePlayer(playerIndex + 1);
        }

        if (playerController.player.GetAbility() != null && uiAbility != null)
        {
            uiAbility.SetAbility(playerController.player.GetAbility().currentAbility);
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && playerIndex < (squadSize - 1))
        {
            changePlayer(playerIndex + 1);
        }

        if (Input.GetMouseButtonDown(1))
        {
            playerController.player.UseAbility(GetComponent<Collider2D>(), playerController.player.transform.position, crosshairs.transform.position);
        }

        if (playerController.player.GetAbility() != null && Input.GetMouseButton(0))
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
