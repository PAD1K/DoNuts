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
    [SerializeField] private EnemyReturn _enemyReturn;
    private bool _isChasing;
    private bool _isTargetInRadius;
    private float _chaseEndTime;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<EnemyReturn>(out _enemyReturn);
        _isChasing = true;
        _chaseEndTime = _chaseTime;
    }

    // Update is called once per frame
    public override State RunCurrentState()
    {
        if(_isChasing)
        {
            if (_chaseEndTime > 0 || _isTargetInRadius)
            {
                _chaseEndTime -= Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.transform.position.x,transform.position.y,_target.transform.position.z), _chaseSpeed * Time.deltaTime);
                //return this;
            }
            else{
                _chaseEndTime = _chaseTime;
                _isChasing = false;
                //return _enemyPatrol;
            }
            return this;
        }
        else{
            _isChasing = true;
            return _enemyReturn;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            _isTargetInRadius = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _isTargetInRadius = false;
        }
    }
}
