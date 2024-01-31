using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void EnterState(StateController enemy);
    public abstract void UpdateState(StateController enemy);

}
