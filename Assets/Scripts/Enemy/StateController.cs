using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField] private State _currentState;
    private EnemyPatrol _patrolState;
    private EnemyChase _chaseState;
    private EnemyReturn _returnState;
    // Update is called once per frame
    public EnemyPatrol PatrolState
    {
        get {return _patrolState;}
    }

    public EnemyChase ChaseState
    {
        get {return _chaseState;}
    }

    public EnemyReturn ReturnState
    {
        get {return _returnState;}
    }
    void Awake()
    {
        TryGetComponent<EnemyPatrol>(out _patrolState);
        TryGetComponent<EnemyChase>(out _chaseState);
        TryGetComponent<EnemyReturn>(out _returnState);
        _currentState = _patrolState;
        _currentState.EnterState(this);
    }
    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(State nextState)
    {
        Debug.Log("State switched to " + nextState);
        _currentState = nextState;
        nextState.EnterState(this);
    }
}
