using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyPatrol : State
{
    [SerializeField] List<Transform> _patrolPoints = new List<Transform>();
    [SerializeField] int _targetPoint;
    [SerializeField] float _speed;
    [SerializeField] private  Transform _waypointsHolder;
    private bool _isTargetInRadius;

    public Vector3 TargetPoint
    {
        get {return _patrolPoints[_targetPoint].position;}
    }

    public bool IsTargetInRadius
    {
        get {return _isTargetInRadius;}
        set {_isTargetInRadius = value;}
    }

    void Start()
    {
        GetWayPoints();
        _targetPoint = 0;
    }
    
    public override void EnterState(StateController enemy)
    {
        _isTargetInRadius = false;
    }

    public override void UpdateState(StateController enemy)
    {
        if(_isTargetInRadius)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        else{
            Patrol();
        }
    }

    void GetWayPoints()
    {
        _patrolPoints.Clear();
        foreach(Transform child in _waypointsHolder)
        {
           _patrolPoints.Add(child);
        }
    }
    void Patrol()
    {
         if(transform.position == _patrolPoints[_targetPoint].position)
        {
            _targetPoint++;
            if(_targetPoint >= _patrolPoints.Count)
            {
                _targetPoint = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, _patrolPoints[_targetPoint].position,_speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _isTargetInRadius = true;
        }
    }
}
