using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturn : State
{
    //[SerializeField] private EnemyChase _enemyChase;
    [SerializeField] float _returnSpeed;
    private Vector3 _patrolPoint;

    public override void EnterState(StateController enemy)
    {
        _patrolPoint =  enemy.PatrolState.TargetPoint;
    }

    // Update is called once per frame
    public override void UpdateState(StateController enemy)
    {
        if(_patrolPoint == transform.position)
        {
            enemy.SwitchState(enemy.PatrolState);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_patrolPoint.x,transform.position.y,_patrolPoint.z), _returnSpeed * Time.deltaTime);
            if(enemy.PatrolState.IsTargetInRadius)
            {
                enemy.SwitchState(enemy.ChaseState);
            }
        }
    }
}
