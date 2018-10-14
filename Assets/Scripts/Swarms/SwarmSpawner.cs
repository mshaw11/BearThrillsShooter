using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SwarmSpawner : MonoBehaviour {

    [SerializeField]
    private int swarmCount;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private SwarmMemberConfig config;
    [SerializeField]
    private Transform swarmControllerPrefab;
    [SerializeField]
    private Transform swarmMemberPrefab;

    private SwarmController swarmController;

    // Use this for initialization
    void Start()
    {
        var swarmController = SwarmController.CreateNew(transform, swarmControllerPrefab, swarmMemberPrefab, swarmCount, target, config);
    }   



}
