using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _velocityValue = 10;
    [SerializeField] private float _angle = 45f;
    [SerializeField] TrajectoryRenderer _trajectoryRenderer;
    private Rigidbody _rigidbody;

    private void Awake() 
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public void Throw(Vector3 direction)
    {
        direction.x *= Mathf.Cos(_angle * Mathf.Deg2Rad);
        direction.y *= Mathf.Sin(_angle * Mathf.Deg2Rad);

        Vector3 velocity = direction 
                        * _velocityValue
                        * Mathf.Abs(Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2)));
        _rigidbody.AddForce(velocity, ForceMode.VelocityChange);
        
        _trajectoryRenderer.ShowTrajectory(transform.position, velocity);
    }
}
