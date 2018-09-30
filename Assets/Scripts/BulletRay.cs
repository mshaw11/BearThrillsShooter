using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRay : MonoBehaviour {

    public float speed = 20;
    public float range = 100;
    public Vector3 endPosition = Vector3.zero;

    private Vector3 startPosition;
    private float lineLength;


    private void Start()
    {
       
        // Set the start position so we can calculate 
        startPosition = transform.position;
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            throw new System.Exception("Game object does not have line render component");
        }
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
}
