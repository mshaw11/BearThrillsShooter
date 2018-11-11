using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmMemberConfig : MonoBehaviour {

    public int swarmMaxLimit = 30;
    public int swarmRespawnSize = 5;

    public float maxFOV = 120;
    public float maxVelocity = 10;
    public float maxForce = 30;

    public float pathfindPriority = 30;
    public float cohesionPriority = 30;
    public float alignmentPriority = 30;
    public float separationPriority = 30;
    public float avoidancePriority = 30;

    public float cohesionRadius = 2;
    public float alignmentRadius = 5;
    public float separationRadius = 5;
    public float avoidanceRadius = 40;

    public float attackDistance = 5;
    public float attackForce = 5;
    public float attackDamage = 10;
    public float maxLineOfSight = 100;
}
