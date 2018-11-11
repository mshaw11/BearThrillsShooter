using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwarmMember : Enemy {

    private Vector3 force;
    private Rigidbody2D rigidBody;
    private SwarmController controller;
    private SwarmMemberConfig conf;
    private Transform target { get; set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void SetSwarmController(SwarmController swarmController)
    {
        this.controller = swarmController;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Init(SwarmController controller, SwarmMemberConfig conf, Transform target)
    {
        this.controller = controller;
        this.conf = conf;
        this.target = target;
    }


    private void LookAtDirection(Vector3 targetDirection)
    {
        var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void LookAtPosition(Vector3 targetPosition)
    {
        var dir = targetPosition - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            if (Vector2.Distance(target.position, transform.position) > conf.attackDistance)
            {
                Vector3 swarmingForce = MoveTowardsPlayer();
                rigidBody.AddForce(swarmingForce);
                LookAtDirection(swarmingForce);
            }
            else
            {
                Vector3 attackingForce = AttackPlayer();
                rigidBody.AddForce(attackingForce);
                LookAtDirection(attackingForce);
            }

            //Limit force application
            if (rigidBody.velocity.magnitude > conf.maxVelocity)
            {
                rigidBody.velocity = rigidBody.velocity.normalized * conf.maxVelocity;
            }

            LookAtPosition(target.transform.position);
        }
    }

    private void SetRotation()
    {
        var dir = target.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected Vector3 AttackPlayer()
    {
        Vector3 directionToPlayer = target.position - transform.position;
        Vector3 attackForce = directionToPlayer.normalized * conf.attackForce;
        return attackForce;
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

    Vector3 RunAway(Vector3 target)
    {
        Vector3 neededVelocity = (((Vector3)rigidBody.position) - target).normalized * conf.maxForce;
        return neededVelocity - (Vector3)rigidBody.velocity;
    }

    virtual protected Vector3 MoveTowardsPlayer()
    {
        var radii = new List<float>() { conf.cohesionRadius, conf.alignmentPriority, conf.separationPriority, conf.avoidancePriority};
        var maxRadius = radii.Max();
        var neighboursShortList = controller.GetNeighbours(this, maxRadius);

        Vector3 finalVec =
            conf.cohesionPriority * Cohesion(neighboursShortList) +
            conf.alignmentPriority * Alignment(neighboursShortList) +
            conf.separationPriority * Separation(neighboursShortList);
        //conf.avoidancePriority * Avoidance();
        if (HasLineOfSightOnTarget())
        {
            //Attack
            var targetDirection = Vector3.Normalize(target.position - transform.position);
            finalVec += conf.pathfindPriority * targetDirection;
        }
        else
        {
            finalVec += conf.pathfindPriority* FollowSwarmLeader();
        }
        return finalVec;
    }

    bool HasLineOfSightOnTarget()
    {
        int layerMask = LayerMask.GetMask("Abilities", "Bullets", "Floor", "Enemies");
        layerMask = ~layerMask;
        var targetDirection = Vector3.Normalize(target.position - transform.position);

        var hit = Physics2D.Raycast(transform.position, targetDirection, conf.maxLineOfSight, layerMask);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Players"))
            {
                return true;
            }
        }
        return false;
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

    static public SwarmMember CreateNew(Transform swarmMemberPrefab, SwarmController controller, SwarmMemberConfig config, Transform target)
    {
        var swarmPrefab = Instantiate(swarmMemberPrefab, controller.transform.position, Quaternion.identity);
        var swarmMember = swarmPrefab.GetComponent<SwarmMember>();
        swarmMember.Init(controller, config, target);
        return swarmMember;
    }

    protected override void die()
    {
        controller.MemberDied(this);
        base.die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            var character = collision.gameObject.GetComponent<Character>();
            character.takeDamage(conf.attackDamage, Assets.Scripts.DamageType.PHYSICAL);
        }
    }

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
