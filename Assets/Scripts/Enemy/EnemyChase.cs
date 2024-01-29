using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyChase : State
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _chaseTime;
    [SerializeField] private float _chaseStartArea;
    [SerializeField] private float _chaseSpeed;
    private bool _isChasing;
    private bool _isTargetStillInRadius;
    private float _chaseEndTime;

    public override void EnterState(StateController enemy)
    {
        _isChasing = true;
        _chaseEndTime = _chaseTime;
    }
    public override void UpdateState(StateController enemy)
    {
        if(_isChasing)
        {
            if (_chaseEndTime > 0 || _isTargetStillInRadius)
            {
                _chaseEndTime -= Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.transform.position.x,transform.position.y,_target.transform.position.z), _chaseSpeed * Time.deltaTime);
            }
            else{
                _isChasing = false;
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
