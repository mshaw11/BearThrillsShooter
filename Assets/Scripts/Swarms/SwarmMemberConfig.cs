using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmMemberConfig : MonoBehaviour {

    public float maxFOV;
    public float maxAcceleration;
    public float maxVelocity;
    public float maxForce;

    //Pathfind
    public float pathfindPriority;

    //Cohesion
    public float cohesionRadius;
    public float cohesionPriority;

    //Alignment
    public float alignmentRadius;
    public float alignmentPriority;

    //Separation
    public float separationRadius;
    public float separationPriority;

    //Avoidance
    public float avoidanceRadius;
    public float avoidancePriority;

}
