using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;


public class SwarmController : MonoBehaviour {

    private List<Character> characters;
    private int swarmCount;
    private List<SwarmMember> swarmMembers;
    private SwarmMemberConfig config;
    private Transform currentTarget;

    void Start () {
        var seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, currentTarget.position, OnPathComplete);
    }

    public static SwarmController CreateNew(Transform spawnPosition, Transform swarmControllerPrefab, Transform swarmMemberPrefab, int swarmCount, List<Character> characters, SwarmMemberConfig config)
    {
        var swarmPrefab = Instantiate(swarmControllerPrefab, swarmControllerPrefab.position, Quaternion.identity);
        var swarmController = swarmPrefab.GetComponent<SwarmController>();
        var destinationSetter = swarmController.GetComponent<AIDestinationSetter>();
        swarmController.Init(spawnPosition, swarmCount, characters, config);
        var closestCharacter = swarmController.FindClosestSquadMember();
        destinationSetter.target = closestCharacter.transform;
        swarmController.currentTarget = closestCharacter.transform;
        for (int i = 0; i < swarmCount; i++)
        {
            var swarmMember = SwarmMember.CreateNew(swarmMemberPrefab, swarmController, config, swarmController.currentTarget);
            swarmController.swarmMembers.Add(swarmMember);
        }
        return swarmController;
    }

    public void Init(Transform spawnPosition, int swarmCount, List<Character> characters, SwarmMemberConfig config)
    {
        this.transform.position = spawnPosition.position;
        this.swarmCount = swarmCount;
        this.characters = characters;
        this.config = config;
        swarmMembers = new List<SwarmMember>();




    }

    private Character FindClosestSquadMember()
    {
        var closestCharacter = characters[0];
        var closestDistance = Vector3.Distance(characters[0].transform.position, transform.position);
        foreach (var character in characters)
        {
            if (Vector3.Distance(character.transform.position, this.transform.position) < closestDistance)
            {
                closestCharacter = character;
            }
        }
        return closestCharacter;
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
        if (p.error)
        {
            Debug.Log("Path error: " + p.errorLog);
        }

    }

    public void MemberDied(SwarmMember member)
    {
        swarmMembers.Remove(member);
    }

}




