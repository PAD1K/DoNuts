using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : StateMachineBehaviour
{
    [SerializeField] List<Transform> _patrolPoints = new List<Transform>();
    [SerializeField] int _targetPoint = 0;
    [SerializeField] float _speed;
    [SerializeField] private  InfoForStateMachine _enemyStatus;
    [SerializeField] private  Transform _enemyTransform;
    [SerializeField] private  bool _isBrave;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _enemyStatus = animator.GetComponentInParent<InfoForStateMachine>();
       _enemyTransform = _enemyStatus.GetComponent<Transform>();
       _patrolPoints = _enemyStatus.SetWaypointsList(_patrolPoints);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(_enemyStatus.IsTargetInRadius)
        {
            animator.SetTrigger("IsInRadius");
            animator.SetBool("IsBrave", _isBrave);
        }
        else{
            Patrol();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    void Patrol()
    {
         if(_enemyTransform.position == _patrolPoints[_targetPoint].position)
        {
            _targetPoint++;
            if(_targetPoint >= _patrolPoints.Count)
            {
                _targetPoint = 0;
            }
        }
        _enemyStatus.LastPatrolPoint = _patrolPoints[_targetPoint].position;
        _enemyTransform.position = Vector3.MoveTowards(_enemyTransform.position, 
        _patrolPoints[_targetPoint].position,
        _speed * Time.deltaTime);
    }
}
