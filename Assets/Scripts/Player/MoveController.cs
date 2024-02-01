using UnityEngine;

public class MoveController : MonoBehaviour
{
    public delegate void LandHandler ();
    public static event LandHandler OnLandEvent;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _height = 0.6f;
    private bool _wasGrounded;
    private Vector3 _velocity;

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
    private void FixedUpdate()
    {
        if (IsGrounded() && !_wasGrounded)
        {
            OnLandEvent?.Invoke();
        }
        
        _wasGrounded = IsGrounded();
    }
}
