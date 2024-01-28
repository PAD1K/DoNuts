using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public delegate void LandHandler ();
    public static event LandHandler OnLandEvent;
    [SerializeField] private float _velocityValue = 10;
    [SerializeField] private float _angle = 45f;
    [SerializeField] TrajectoryRenderer _trajectoryRenderer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _height = 0.6f; 
    private Rigidbody _rigidbody;
    private bool _wasGrounded;

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

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _height, _groundLayer);
    }

    private void FixedUpdate()
    {
        if (IsGrounded() && !_wasGrounded)
        {
            OnLandEvent?.Invoke();
        }
        
        _wasGrounded = IsGrounded();
    }
}
