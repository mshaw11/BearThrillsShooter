using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speedMultiplier;
    public float sprintMultiplier;
    public float jumpMultiplier;
    public float jumpCooldown;
    public float throwForce = 45f;

    public Crosshairs crosshairs;
    public GameObject grenadePrefab;
    public GameObject AreaOfEffectType;

    private float nextJump;
    private float angle;
    private Rigidbody2D rigidBody;

    Camera viewCamera;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        nextJump = 0.0f;
        angle = 0.0f;
        viewCamera = Camera.main;
        Cursor.visible = false;
    }

    // Independent of frame rate
    private void FixedUpdate()
    {
        // Player Rotation - based on mouse cursor location
        var playerPos = rigidBody.position;
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var direction = new Vector2(cameraPosition.x - playerPos.x, cameraPosition.y - playerPos.y);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rigidBody.rotation = angle;

        //Debug.Log(angle);
        //Debug.DrawLine(new Vector3(pos.x, pos.y, -1), new Vector3(cameraPosition.x, cameraPosition.y, -1), Color.red, 2.5f);

        float horizontal = Input.GetAxis("Horizontal") * speedMultiplier;
        float vertical = Input.GetAxis("Vertical") * speedMultiplier;

        if (Input.GetKeyUp(KeyCode.F) && Time.time > nextJump)
        {
            nextJump = Time.time + jumpCooldown;
            // For arugment 1 (Force): 
            //  - Either use direction vector to dodge towards mouse position 
            //    or 
            //  - horizontal /vertical to dodge in direction of movement
            rigidBody.AddForceAtPosition(new Vector2(direction.x, direction.y) * jumpMultiplier, rigidBody.position);
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                horizontal *= sprintMultiplier;
                vertical *= sprintMultiplier;
            }

            rigidBody.velocity = new Vector2(horizontal, vertical);
        }

        //Get ray from camera to ground plane
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.back, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector2 point = ray.GetPoint(rayDistance);
            crosshairs.transform.position = point;
            //crosshairs.DetectTargets(ray);
            //Debug.DrawRay(transform.position, forward, Color.green);
        }

        if(Input.GetMouseButtonDown(0))
        {
            ThrowGrenade();
        }

        if (Input.GetMouseButtonDown(1))
        {
            AOE();
        }

    }

    void ThrowGrenade()
    {
        Vector3 pos = transform.position;
        pos.z -= 3;
        GameObject grenade = Instantiate(grenadePrefab, pos, Quaternion.identity);
        Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();
        rb.AddForce((crosshairs.transform.position - transform.position) * throwForce);
    }

    void AOE()
    {
        Vector3 pos = transform.position;
        pos.z -= 3;
        Instantiate(AreaOfEffectType, pos, Quaternion.identity);
        AreaOfEffectType.GetComponent<Rigidbody2D>();
        
    }
}
