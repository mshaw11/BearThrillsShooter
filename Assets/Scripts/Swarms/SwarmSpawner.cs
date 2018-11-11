using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SwarmSpawner : MonoBehaviour {

    [SerializeField]
    private SwarmMemberConfig config;
    [SerializeField]
    private Transform swarmControllerPrefab;
    [SerializeField]
    private Transform swarmMemberPrefab;
    [SerializeField]
    private MovementManager movementManager;

    

    // Use this for initialization
    void Start()
    {
        var squadMembers = movementManager.GetSquadMembers();
        var swarmController = SwarmController.CreateNew(transform, swarmControllerPrefab, swarmMemberPrefab, squadMembers, config);
    }   



}
