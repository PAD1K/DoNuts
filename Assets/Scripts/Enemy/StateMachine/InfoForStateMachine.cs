using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoForStateMachine : MonoBehaviour
{
    [SerializeField] private  Transform _waypointsHolder;
    private bool _isTargetInRadius;
    private Vector3 _patrolPoint;
    public bool IsTargetInRadius
    {
        get {return _isTargetInRadius;}
        set {_isTargetInRadius = value;}
    }

    public Vector3 LastPatrolPoint
    {
        get {return _patrolPoint;}
        set {_patrolPoint = value;}
    }
    public List<Transform> SetWaypointsList(List<Transform> patrolPoints)
    {
        patrolPoints.Clear();
        foreach(Transform child in _waypointsHolder)
        {
           patrolPoints.Add(child);
        }
        return patrolPoints;
    }

    void OnTriggerEnter(Collider other)
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
