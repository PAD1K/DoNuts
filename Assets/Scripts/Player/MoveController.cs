using UnityEngine;

public class MoveController : MonoBehaviour
{
    public delegate void LandHandler ();
    public static event LandHandler OnLandEvent;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _height = 0.6f; 
    [SerializeField] private float _stunTime = 5;
    private Rigidbody _rigidbody;
    private bool _wasGrounded;
    private Vector3 _velocity;
    private bool _isStun = false;
    private float _startStunTime = 0;

    public bool IsPlayerGrounded
    {
        get{ return IsGrounded();}
    }

    public Vector3 JumpVelocity
    {
        get{ return _velocity;}
        set{ _velocity = value;}
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _height, _groundLayer);
    }

    public void Stun()
    {
        if (_isStun)
        {
            return;
        }
        
        _isStun = true;
        _startStunTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (IsGrounded() && !_wasGrounded)
        {
            OnLandEvent?.Invoke();
        }

        if (_isStun && Time.time > _startStunTime + _stunTime)
        {
            _isStun = false;
        }
        
        _wasGrounded = IsGrounded();
    }
}
