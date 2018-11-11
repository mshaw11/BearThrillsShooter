using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;

    [SerializeField]
    private float _smoothTime = 0.3f;

    private Vector3 _currentPosition = new Vector3();

    private Vector3 _currentVelocity = new Vector3();

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _currentPosition.Set(_target.position.x, _target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, _currentPosition, ref _currentVelocity, _smoothTime);
    }
}