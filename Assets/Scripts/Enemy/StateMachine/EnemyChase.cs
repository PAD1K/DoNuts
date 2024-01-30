using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyChase : State
{
    [SerializeField] private float _chaseTime;
    [SerializeField] private float _chaseStartArea;
    [SerializeField] private float _chaseSpeed;
    private GameObject _target;
    private bool _isChasing;
    private bool _isTargetStillInRadius;
    [SerializeField] private float _chaseEndTime;

    void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }
    public override void EnterState(StateController enemy)
    {
        _isChasing = true;
        Debug.Log("entered");
        _chaseEndTime = _chaseTime;
    }
    public override void UpdateState(StateController enemy)
    {
        if(_isChasing)
        {
            if (_chaseEndTime > 0 || _isTargetStillInRadius)
            {
                _chaseEndTime -= Time.deltaTime;
                //Debug.Log(_target.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.transform.position.x,transform.position.y,_target.transform.position.z), _chaseSpeed * Time.deltaTime);
            }
            else{
                _isChasing = false;
                enemy.PatrolState.IsTargetInRadius = false;
            }
        }
        else{
            enemy.SwitchState(enemy.ReturnState);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            _isTargetStillInRadius = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _isTargetStillInRadius = false;
        }
    }
}
