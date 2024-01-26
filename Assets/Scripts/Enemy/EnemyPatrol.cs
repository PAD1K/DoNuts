using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Transform[] _patrolPoints;
    [SerializeField] int _targetPoint;
    [SerializeField] float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _targetPoint = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == _patrolPoints[_targetPoint].position)
        {
            _targetPoint++;
            if(_targetPoint >= _patrolPoints.Length)
            {
                _targetPoint = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, _patrolPoints[_targetPoint].position,_speed * Time.deltaTime);
    }
}
