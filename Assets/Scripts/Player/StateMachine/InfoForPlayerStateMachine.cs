using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoForPlayerStateMachine : MonoBehaviour
{
    public delegate void LandHandler ();
    public static event LandHandler OnLandEvent;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _height = 0.6f; 
    [SerializeField] private float _stunTime = 5;
    private bool _isStun = false;
    private float _startStunTime = 0;
    private bool _wasGrounded;
    // public bool IsPlayerGrounded
    // {
    //     get{ return IsGrounded();}
    // }

    private Vector2 _moveVector;
    public Vector2 MoveVector
    {
        get{return _moveVector;}
        set{_moveVector = value;}
    }
    private bool _isAiming;
    public bool IsAimingCanceled
    {
        get{return _isAiming;}
        set{_isAiming = value;}
    }
    private Vector3 _velocity;

    public Vector3 JumpVelocity
    {
        get{ return _velocity;}
        set{ _velocity = value;}
    }

    public bool IsPlayerGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _height, _groundLayer);
    }

    private void FixedUpdate()
    {
        if (IsPlayerGrounded() && !_wasGrounded)
        {
            OnLandEvent?.Invoke();
        }

        if (_isStun && Time.time > _startStunTime + _stunTime)
        {
            _isStun = false;
        }
        
        _wasGrounded = IsPlayerGrounded();
    }
}
