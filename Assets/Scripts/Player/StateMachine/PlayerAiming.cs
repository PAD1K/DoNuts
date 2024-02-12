using UnityEngine;

public class PlayerAiming : StateMachineBehaviour
{
    [SerializeField] private float _velocityValue;
    [SerializeField] private float _angle;
    [SerializeField] private float _stickSensitivity = 0.5f;

    //private Controller _infoForPlayerStateMachine;
    private InfoForPlayerStateMachine _infoForPlayerStateMachine;

    private Transform _playerTransform;
    private TrajectoryRenderer _tragectoryRenderer;
    
    private Vector3 _direction = new Vector3(1, 1, 1);
    //private MoveController _infoForPlayerStateMachine;
    private Vector3 _velocity;
    private Vector3 _aimDirection;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("On enter " + _velocity);
        _velocity = Vector3.zero;
        _infoForPlayerStateMachine = animator.GetComponentInParent<InfoForPlayerStateMachine>();
        //_infoForPlayerStateMachine = animator.GetComponentInParent<Controller>();
        _playerTransform = _infoForPlayerStateMachine.GetComponent<Transform>();
        _tragectoryRenderer = _infoForPlayerStateMachine.GetComponentInChildren<TrajectoryRenderer>();
        //_infoForPlayerStateMachine = _infoForPlayerStateMachine.GetComponent<MoveController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _direction.x = -_infoForPlayerStateMachine.MoveVector.x;
        _direction.z = -_infoForPlayerStateMachine.MoveVector.y;
        Aim(_direction, animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(_velocity);
        _infoForPlayerStateMachine.IsAimingCanceled = false;
       _tragectoryRenderer.HideTrajectory();
    }

    private void Aim(Vector3 direction, Animator animator)
    {        
        Debug.Log(_infoForPlayerStateMachine.IsAimingCanceled);
        if(_infoForPlayerStateMachine.IsAimingCanceled)
        {
            if (Mathf.Abs(_velocity.x) <= _stickSensitivity && Mathf.Abs(_velocity.z) <= _stickSensitivity)
            {
                animator.SetTrigger("JumpCanceled");
            }
            else
            {
                _infoForPlayerStateMachine.JumpVelocity = _velocity;
                animator.SetTrigger("PlayerJumped");
            }
        }
        else{
            _aimDirection.x = direction.x * Mathf.Cos(_angle * Mathf.Deg2Rad);
            _aimDirection.y = direction.y;
            _aimDirection.z = direction.z * Mathf.Sin(_angle * Mathf.Deg2Rad);
            _velocity = _aimDirection 
                        * _velocityValue
                        * Mathf.Abs(Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2)));
            _tragectoryRenderer.ShowTrajectory(_playerTransform.position, _velocity);
            
        }
    }
}
