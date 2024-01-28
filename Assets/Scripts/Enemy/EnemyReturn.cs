using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturn : State
{
    [SerializeField] private EnemyPatrol _enemyPatrol;
    [SerializeField] private EnemyChase _enemyChase;
    [SerializeField] float _returnSpeed;
    private Vector3 _patrolPoint;
    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<EnemyPatrol>(out _enemyPatrol);
        TryGetComponent<EnemyChase>(out _enemyChase);
    }

    // Update is called once per frame
    public override State RunCurrentState()
    {
        _patrolPoint = _enemyPatrol.TargetPoint;
        //return this;
        if(_patrolPoint == transform.position)
        {
            return _enemyPatrol;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_patrolPoint.x,transform.position.y,_patrolPoint.z), _returnSpeed * Time.deltaTime);
            if(_enemyPatrol.IsTargetInRadius)
            {
                _enemyPatrol.IsTargetInRadius = false;
                return _enemyChase;
            }
            else{
                return this;
            }
        }
    }
}
