using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class OvalRouteCreator : MonoBehaviour
{
    private List<Vector3> _ellipsePoints;
    [SerializeField] private GameObject _waypointPrefab;
    [SerializeField] private float _widthOfEllipse;
    [SerializeField] private float _lengthOfEllipse;
    [SerializeField] private int _numberOfPoints;
    private GameObject _waypoint;
    // Start is called before the first frame update
    void Awake()
    {
        _ellipsePoints = GenerateEllipse(_widthOfEllipse,_lengthOfEllipse,_numberOfPoints);
        for(int i = 0; i < _ellipsePoints.Count; i++)
        {
            CreateWaypoint(_ellipsePoints[i]);
        }
        //CreateWaypoint(new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateWaypoint(Vector3 _waypointPosition)
    {
        _waypoint = Instantiate(_waypointPrefab);
        _waypoint.transform.SetParent(this.transform);
        _waypoint.transform.position = _waypointPosition;
    }

    private List<Vector3> GenerateEllipse(float _a, float _b, int _numberOfPoints)
    {
        Debug.Log("a=" + _a + " b=" + _b);
        List<Vector3> points = new List<Vector3>();
        // for(int i = 0; i < _number_of_points; i++)
        // {
        //     float t = (float)i / (float)_number_of_points * 2.0f * Mathf.PI;
        //     points.Add(new Vector3(_a * Mathf.Cos(t), 0, _b * Mathf.Sin(t)));
        // }
        for (int i = 0; i <= _numberOfPoints; i++)
        {
            float angle = (float)i / (float)_numberOfPoints * 2f * Mathf.PI;
            float x = Mathf.Cos(angle) * _a + (transform.position.x - _a);
            float y = Mathf.Sin(angle) * _b + transform.position.z;
            Debug.Log(i + ". " + "x=" + x + "; y=" + y);
            //lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
            points.Add(new Vector3(x, 0.5f, y));
        }
        return points;
    }
}
