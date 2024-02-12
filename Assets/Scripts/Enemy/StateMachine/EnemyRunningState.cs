using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class EnemyRunningState : StateMachineBehaviour
{
    [SerializeField] private float _runningSpeed;
    [SerializeField] private float _runningTime;
    [SerializeField] private float _displacementDistance;
    [SerializeField] private  InfoForEnemyStateMachine _enemyStatus;
    [SerializeField] private  Transform _enemyTransform;
    [SerializeField] private float _runningEndTime;
    private GameObject _target;
    private bool _isRunning;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _isRunning = true;
        _runningEndTime = _runningTime;
        _target = GameObject.FindGameObjectWithTag("Player");
        _enemyStatus = animator.GetComponentInParent<InfoForEnemyStateMachine>();
        _enemyTransform = _enemyStatus.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(_isRunning)
        {
            if (_runningEndTime > 0 || _enemyStatus.IsTargetInRadius)
            {
                _runningEndTime -= Time.deltaTime;
                //Debug.Log(_target.transform.position);
                Vector3 directionFromPlayer = (_enemyTransform.position - _target.transform.position).normalized;
                _enemyTransform.position = Vector3.MoveTowards(_enemyTransform.position, 
                new Vector3(_target.transform.position.x + (directionFromPlayer.x * _displacementDistance),
                    _enemyTransform.position.y,
                    _target.transform.position.z + (directionFromPlayer.z * _displacementDistance)),
                 _runningSpeed * Time.deltaTime);
            }
            else{
                _isRunning = false;
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
