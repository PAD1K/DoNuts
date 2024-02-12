using UnityEngine;

public class Playeridle : StateMachineBehaviour
{
    private Controller _playerInput;
    private MoveController _moveController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerInput = animator.GetComponentInParent<Controller>();
        _moveController = _playerInput.GetComponent<MoveController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsGrounded", _moveController.IsPlayerGrounded);
       if(_playerInput.MoveVector != Vector2.zero && _moveController.IsPlayerGrounded)
       {
            animator.SetTrigger("IsAiming");
       }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
