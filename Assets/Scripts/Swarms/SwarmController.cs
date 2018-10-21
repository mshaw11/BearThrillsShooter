using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;


public class SwarmController : MonoBehaviour {

    private Transform target;
    private int swarmCount;
    private List<SwarmMember> swarmMembers;
    private SwarmMemberConfig config;

    void Start () {
        Seeker seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    public static SwarmController CreateNew(Transform spawnPosition, Transform swarmControllerPrefab, Transform swarmMemberPrefab, int swarmCount, Transform target, SwarmMemberConfig config)
    {
        var swarmPrefab = Instantiate(swarmControllerPrefab, swarmControllerPrefab.position, Quaternion.identity);
        var swarmController = swarmPrefab.GetComponent<SwarmController>();
        swarmController.Init(spawnPosition, swarmCount, target, config);
        for (int i = 0; i < swarmCount; i++)
        {
            var swarmMember = SwarmMember.CreateNew(swarmMemberPrefab, swarmController, config);
            swarmController.swarmMembers.Add(swarmMember);
        }
        return swarmController;
    }
     
    private void Init(Transform spawnPosition, int swarmCount, Transform target, SwarmMemberConfig config)
    {
        this.transform.position = spawnPosition.position;
        this.swarmCount = swarmCount;
        this.target = target;
        this.config = config;
        swarmMembers = new List<SwarmMember>();
    }


    public List<SwarmMember> GetNeighbours(SwarmMember member, float radius)
    {
        List<SwarmMember> neighboursFound = new List<SwarmMember>();
        foreach (SwarmMember otherMember in swarmMembers)
        {
            if (otherMember == member)
                continue;
            if (Vector3.Distance(member.GetPosition(), otherMember.GetPosition()) <= radius)
            {
                neighboursFound.Add(otherMember);
            }
        }
        return neighboursFound;
    }

    public List<SwarmMember> GetNeighboursUsingShortList(List<SwarmMember> shortList, SwarmMember member, float radius)
    {
        List<SwarmMember> neighboursFound = new List<SwarmMember>();
        foreach (SwarmMember otherMember in shortList)
        {
            if (otherMember == member)
                continue;
            if (Vector3.Distance(member.GetPosition(), otherMember.GetPosition()) <= radius)
            {
                neighboursFound.Add(otherMember);
            }
        }
        return neighboursFound;
    }

    public List<Enemy> GetEnemies(SwarmMember member, float radius)
    {
        List<Enemy> returnEnemies = new List<Enemy>();
        //foreach (var enemy in enemies)
        //{
        //    if (Vector3.Distance(member.GetPosition(), enemy.rigidBody.position) <= radius)
        //    {
        //        returnEnemies.Add(enemy);
        //    }
        //}
        //return returnEnemies;
        return returnEnemies;
    }


    private void OnPathComplete(Path p)
    {
        Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
    }

    public void MemberDied(SwarmMember member)
    {
        swarmMembers.Remove(member);
    }
}




