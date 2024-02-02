using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturnState : StateMachineBehaviour
{
    [SerializeField] float _returnSpeed;
    [SerializeField] private  InfoForStateMachine _enemyStatus;
    [SerializeField] private  Transform _enemyTransform;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemyStatus = animator.GetComponentInParent<InfoForStateMachine>();
        _enemyTransform = _enemyStatus.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(_enemyStatus.LastPatrolPoint == _enemyTransform.position)
        {
            animator.SetTrigger("ReturnedToPatrol");
        }
        else
        {
            _enemyTransform.position = Vector3.MoveTowards(_enemyTransform.position, 
            new Vector3(_enemyStatus.LastPatrolPoint.x,
                _enemyTransform.position.y,
                _enemyStatus.LastPatrolPoint.z), 
            _returnSpeed * Time.deltaTime);
            if(_enemyStatus.IsTargetInRadius)
            {
                animator.SetTrigger("IsInRadius");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
