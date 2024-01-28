using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField] private State _currentState;
    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState = _currentState?.RunCurrentState();

        if(nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(State nextState)
    {
        _currentState = nextState;
    }
}
