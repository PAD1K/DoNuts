using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer;

    public void ShowTrajectory(Vector3 origin, Vector3 velocity)
    {
        Vector3[] points = new Vector3[100];

        _lineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + velocity * time + Physics.gravity * time * time / 2;
        }
        
        _lineRenderer.SetPositions(points);
    }
}
