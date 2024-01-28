using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class MoveController : MonoBehaviour
{
    public delegate void LandHandler ();
    public static event LandHandler OnLandEvent;
    [SerializeField] private float _velocityValue = 10;
    [SerializeField] private float _angle = 45f;
    [SerializeField] TrajectoryRenderer _trajectoryRenderer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _height = 0.6f; 
    [SerializeField] private float _stickSensitivity = 0.5f;
    private Rigidbody _rigidbody;
    private bool _wasGrounded;
    private Vector3 _direction;
    private Vector3 _velocity;

    public void Throw()
    {
        if (!IsGrounded()) 
        {
            return;
        }

        if (Mathf.Abs(_velocity.x) <= _stickSensitivity && Mathf.Abs(_velocity.z) <= _stickSensitivity)
        {
            return;
        }

        _rigidbody.AddForce(_velocity, ForceMode.VelocityChange);
    }

    public void Aim(Vector3 direction)
    {        
        if (!IsGrounded()) 
        {
            return;
        }

        _direction.x = direction.x * Mathf.Cos(_angle * Mathf.Deg2Rad);
        _direction.y = direction.y;
        _direction.z = direction.z * Mathf.Sin(_angle * Mathf.Deg2Rad);

        _velocity = _direction 
                    * _velocityValue
                    * Mathf.Abs(Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2)));

        _trajectoryRenderer.ShowTrajectory(transform.position, _velocity);
    }

    private void Awake() 
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
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
