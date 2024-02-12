using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : StateMachineBehaviour
{
    [SerializeField] private float _chasingSpeed;
    [SerializeField] private float _chaseTime;
    [SerializeField] private  InfoForEnemyStateMachine _enemyStatus;
    [SerializeField] private  Transform _enemyTransform;
    [SerializeField] private float _chaseEndTime;
    private GameObject _target;
    private bool _isChasing;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _isChasing = true;
        _chaseEndTime = _chaseTime;
        _target = GameObject.FindGameObjectWithTag("Player");
        _enemyStatus = animator.GetComponentInParent<InfoForEnemyStateMachine>();
        _enemyTransform = _enemyStatus.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(_isChasing)
        {
            if (_chaseEndTime > 0 || _enemyStatus.IsTargetInRadius)
            {
                _chaseEndTime -= Time.deltaTime;
                _enemyTransform.position = Vector3.MoveTowards(_enemyTransform.position, 
                new Vector3(_target.transform.position.x,
                    _enemyTransform.position.y,_target.
                    transform.position.z),
                    _chasingSpeed * Time.deltaTime);
            }
            else{
                _isChasing = false;
            }
        }
        else{
            animator.SetTrigger("IsNotInRadius");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
