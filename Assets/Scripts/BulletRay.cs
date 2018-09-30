using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRay : MonoBehaviour {

    [SerializeField]
    private float speed = 20;
    [SerializeField]
    private float range = 100;

    private Vector3 startPosition;
    private float lineLength;


    private void Start()
    {
       
        // Set the start position so we can calculate 
        startPosition = transform.position;
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            throw new System.Exception("Game object does not have line renderer component");
        }

        // This is the length of the line drawn which represents a single bullet
        lineLength = (lineRenderer.GetPosition(0) - lineRenderer.GetPosition(1)).magnitude;
    }

    // Update is called once per frame
    void Update () {

        // Move forawrd in local space
        Vector3 direction = Vector3.right * Time.deltaTime * speed;
        transform.Translate (direction);

       
        // If max distance has been traveled since creation, delete object
        if ((startPosition - transform.position).magnitude + (lineLength/2) > range)
        {
            Destroy(gameObject);
        }

    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setRange(float range)
    {
        this.range = range;
    }

    public void updateVariables(float speed, float range)
    {
        setSpeed(speed);
        setRange(range);
    }
}
