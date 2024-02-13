using UnityEngine;

public class PlayerInAir : StateMachineBehaviour
{
    //private Controller _infoForPlayerStateMachine;
    private Rigidbody _rb;
    private Animator _animator;
    private InfoForPlayerStateMachine _infoForPlayerStateMachine;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _animator = animator;
        _infoForPlayerStateMachine = animator.GetComponentInParent<InfoForPlayerStateMachine>();
        //_infoForPlayerStateMachine = animator.GetComponentInParent<Controller>();
        _rb = _infoForPlayerStateMachine.GetComponentInChildren<Rigidbody>();
        //_infoForPlayerStateMachine = _infoForPlayerStateMachine.GetComponent<MoveController>();
        InfoForPlayerStateMachine.OnLandEvent += Landed;
        Throw(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsGrounded", _infoForPlayerStateMachine.IsPlayerGrounded());
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    private void Throw(Animator animator)
    {
        _infoForPlayerStateMachine.IsAimingCanceled = false;
        _rb.AddForce(_infoForPlayerStateMachine.JumpVelocity, ForceMode.VelocityChange);
        animator.SetBool("IsJumping", true);
    }

    void Landed()
    {
        _animator.SetBool("IsJumping", false);
    }
}
