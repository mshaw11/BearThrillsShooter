using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwarmMember : MonoBehaviour {

    private Vector3 force;
    private Rigidbody2D rigidBody;
    private SwarmController controller;
    private SwarmMemberConfig conf;

    Vector3 wanderTarget;



    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0));
    }

    public void SetSwarmController(SwarmController swarmController)
    {
        this.controller = swarmController;
    }

    private void Init(SwarmController controller, SwarmMemberConfig conf)
    {
        this.controller = controller;
        this.conf = conf;
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(Combine());
        //Limit force application
        if (rigidBody.velocity.magnitude > conf.maxVelocity)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * conf.maxVelocity;
        }
    }


    protected Vector3 FollowSwarmLeader()
    {
        var heading = (Vector2)controller.transform.position - rigidBody.position;
        return heading;
    }

    Vector3 Cohesion(List<SwarmMember> neighboursShortList)
    {
        Vector3 cohesionVector = new Vector3();
        int countMembers = 0;
        var neighbours = controller.GetNeighboursUsingShortList(neighboursShortList, this, conf.cohesionRadius);
        if (neighbours.Count == 0)
            return cohesionVector;
        foreach(var member in neighbours)
        {
            if(IsInFOV(rigidBody.position))
            {
                if (member == null)
                {
                    Debug.Log("Member is null!");
                }
                else
                {
                    if (member.rigidBody == null)
                    {
                        Debug.Log("rigid body is null!");
                    }
                    else
                    {
                        if (member.rigidBody.position == null)
                        {
                            Debug.Log("Position is null");
                        }
                    }
                }


                cohesionVector += (Vector3)member.rigidBody.position;
                countMembers++;
            }
        }
        if (countMembers == 0)
            return cohesionVector;
        cohesionVector /= countMembers;
        cohesionVector = cohesionVector - (Vector3)rigidBody.position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }

    Vector3 Alignment(List<SwarmMember> neighboursShortList)
    {
        Vector3 alignVector = new Vector3();
        var members = controller.GetNeighboursUsingShortList(neighboursShortList, this, conf.alignmentRadius);
        if (members.Count == 0)
            return alignVector;
        foreach(var member in members)
        {
            if (IsInFOV(rigidBody.position))
                alignVector += (Vector3)member.rigidBody.velocity;
        }
        return alignVector.normalized;
    }

    Vector3 Separation(List<SwarmMember> neighboursShortList)
    {
        Vector3 separateVector = new Vector3();
        var members = controller.GetNeighboursUsingShortList(neighboursShortList, this, conf.separationRadius);
        if (members.Count == 0)
            return separateVector;

        foreach(var member in members)
        {
            if (IsInFOV(rigidBody.position))
            {
                Vector3 movingTowards = this.rigidBody.position - member.rigidBody.position;
                if (movingTowards.magnitude > 0)
                {
                    separateVector += movingTowards.normalized / movingTowards.magnitude;
                }
            }
        }
        return separateVector.normalized;
    }

    //Vector3 Avoidance(List<SwarmMember> neighboursShortList)
    //{
    //    Vector3 avoidVector = new Vector3();
    //    var enemyList = controller.GetEnemies(this, conf.avoidanceRadius);
    //    if (enemyList.Count == 0)
    //        return avoidVector;
    //    foreach(var enemy in enemyList)
    //    {
    //        avoidVector += RunAway(enemy.rigidBody.position);
    //    }
    //    return avoidVector.normalized;
    //}

    Vector3 RunAway(Vector3 target)
    {
        Vector3 neededVelocity = (((Vector3)rigidBody.position) - target).normalized * conf.maxForce;
        return neededVelocity - (Vector3)rigidBody.velocity;
    }

    virtual protected Vector3 Combine()
    {
        var radii = new List<float>() { conf.cohesionRadius, conf.alignmentPriority, conf.separationPriority, conf.avoidancePriority };
        var maxRadius = radii.Max();
        var neighboursShortList = controller.GetNeighbours(this, maxRadius);

        Vector3 finalVec =
            conf.cohesionPriority * Cohesion(neighboursShortList) +
            conf.pathfindPriority * FollowSwarmLeader() +
            conf.alignmentPriority * Alignment(neighboursShortList) +
            conf.separationPriority * Separation(neighboursShortList);
            //conf.avoidancePriority * Avoidance();

        return finalVec;
    }

    float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }

    bool IsInFOV(Vector3 vec)
    {
        return Vector3.Angle(this.rigidBody.velocity, vec - (Vector3)this.rigidBody.position) <= conf.maxFOV;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }


    static public SwarmMember CreateNew(Transform swarmMemberPrefab, SwarmController controller, SwarmMemberConfig config)
    {
        var swarmPrefab = Instantiate(swarmMemberPrefab, controller.transform.position, Quaternion.identity);
        var swarmMember = swarmPrefab.GetComponent<SwarmMember>();
        swarmMember.Init(controller, config);
        return swarmMember;
    }
}



//protected Vector3 Wander()
//{
//    float jitter = conf.wanderJitter * Time.deltaTime;
//    wanderTarget += new Vector3(RandomBinomial() * jitter, RandomBinomial() * jitter, 0);
//    wanderTarget = wanderTarget.normalized;
//    wanderTarget *= conf.wanderRadius;
//    Vector3 targetInLocalSpace = wanderTarget + new Vector3(conf.wanderDistance, conf.wanderDistance, 0);
//    Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);
//    targetInWorldSpace -= (Vector3)rigidBody.position;
//    return targetInWorldSpace.normalized;
//}
