using UnityEngine;

public class PlayerInAir : StateMachineBehaviour
{
    private Controller _playerInput;
    private Rigidbody _rb;
    private MoveController _moveController;
    private Animator _animator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _animator = animator;
        _playerInput = animator.GetComponentInParent<Controller>();
        _rb = _playerInput.GetComponentInChildren<Rigidbody>();
        _moveController = _playerInput.GetComponent<MoveController>();
        MoveController.OnLandEvent += Landed;
        Throw(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsGrounded", _moveController.IsPlayerGrounded);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    private void Throw(Animator animator)
    {
        _playerInput.IsAimingCanceled = false;
        Debug.Log($"JumpVelocity is {_moveController.JumpVelocity}");
        _rb.AddForce(_moveController.JumpVelocity, ForceMode.VelocityChange);
        animator.SetBool("IsJumping", true);
    }

    void Landed()
    {
        _animator.SetBool("IsJumping", false);
    }
}
