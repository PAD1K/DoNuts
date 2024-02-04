using UnityEngine;

public class PlayerAiming : StateMachineBehaviour
{
    [SerializeField] private float _velocityValue;
    [SerializeField] private float _angle;
    [SerializeField] private float _stickSensitivity = 0.5f;

    private Controller _playerInput;
    private Transform _playerTransform;
    private TrajectoryRenderer _tragectoryRenderer;
    
    private Vector3 _direction = new Vector3(1, 1, 1);
    private MoveController _moveController;
    private Vector3 _velocity;
    private Vector3 _aimDirection;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("On enter " + _velocity);
        _velocity = Vector3.zero;
        _playerInput = animator.GetComponentInParent<Controller>();
        _playerTransform = _playerInput.GetComponent<Transform>();
        _tragectoryRenderer = _playerInput.GetComponentInChildren<TrajectoryRenderer>();
        _moveController = _playerInput.GetComponent<MoveController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _direction.x = -_playerInput.MoveVector.x;
        _direction.z = -_playerInput.MoveVector.y;
        Aim(_direction, animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerInput.IsAimingCanceled = false;
       _tragectoryRenderer.HideTrajectory();
    }

    private void Aim(Vector3 direction, Animator animator)
    {        
        Debug.Log(_playerInput.IsAimingCanceled);
        if(_playerInput.IsAimingCanceled)
        {
            if (Mathf.Abs(_velocity.x) <= _stickSensitivity && Mathf.Abs(_velocity.z) <= _stickSensitivity)
            {
                animator.SetTrigger("JumpCanceled");
            }
            else
            {
                _moveController.JumpVelocity = _velocity;
                animator.SetTrigger("PlayerJumped");
            }
        }
        else{
            _aimDirection.x = direction.x * Mathf.Cos(_angle * Mathf.Deg2Rad);
            _aimDirection.y = direction.y;
            _aimDirection.z = direction.z * Mathf.Sin(_angle * Mathf.Deg2Rad);
            _velocity = _aimDirection 
                        * _velocityValue
                        * Mathf.Abs(Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2)));
            _tragectoryRenderer.ShowTrajectory(_playerTransform.position, _velocity, _angle);
        }
    }
}
