using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Character target;

    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, -1);

    // Use this for initialization
    void Start()
    {
        //target = PlayerMovementController.player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //target = PlayerMovementController.GetPlayer().transform;
        if (target)
        {
            // Always Update to Exactly Targets Position + Offset
            transform.position = new Vector3(
                target.transform.position.x + offset.x,
                target.transform.position.y + offset.y,
                target.transform.position.z + offset.z);
        }
    }
}