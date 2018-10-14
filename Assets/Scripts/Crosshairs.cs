using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshairs : MonoBehaviour {

	// Use this for initialization
	void Start () {
        originalDotColour = dot.color;
	}

    [SerializeField]
    private LayerMask targetMask;
    [SerializeField]
    private SpriteRenderer dot;
    [SerializeField]
    private Color dotHighlightColour;

    Color originalDotColour;
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(Vector3.forward * 40 * Time.deltaTime);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.back, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector2 point = ray.GetPoint(rayDistance);
            transform.position = point;
        }
    }

    public void DetectTargets(Ray ray)
    {
        if (Physics.Raycast(ray, 100, targetMask))
        {
            dot.color = dotHighlightColour;
        }
        else
        {
            dot.color = originalDotColour;
        }
    }
}
