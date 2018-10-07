using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float speedMultiplier;

    [SerializeField]
    private float sprintMultiplier;

    [SerializeField]
    private float jumpMultiplier;

    [SerializeField]
    private float jumpCooldown;

    [SerializeField]
    private Crosshairs crosshairs;

    [SerializeField]
    private AbilityHandler AbilityHandler;


    private float nextJump;
    private float angle;
    private Rigidbody2D rigidBody;

    Camera viewCamera;

    // Use this for initialization
    void Start()
    {
        AbilityHandler = Instantiate(AbilityHandler);
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
        }
        
        if (Input.GetMouseButtonDown(1))
        {

            AbilityHandler.UseAbility(GetComponent<Collider2D>(), transform.position, crosshairs.transform.position);
        }
        
    }
}
