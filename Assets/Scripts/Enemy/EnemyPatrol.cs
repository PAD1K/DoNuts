using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] List<Transform> _patrolPoints = new List<Transform>();
    [SerializeField] int _targetPoint;
    [SerializeField] float _speed;
    [SerializeField] private  Transform _waypointsHolder;
    private GameObject _enemy;
    private GameObject _enemyPath;
    // Start is called before the first frame update
    void Awake()
    {
        _targetPoint = 0;
        //FindPath();
        //_patrolPoints = gameObject.GetComponentsInChildren<Transform>();
        GetWayPoints();
        
    }

    // Update is called once per frame
    void Update()
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

    void GetWayPoints()
    {
        _patrolPoints.Clear();
        foreach(Transform child in _waypointsHolder)
        {
           _patrolPoints.Add(child);
        }
    }
}
