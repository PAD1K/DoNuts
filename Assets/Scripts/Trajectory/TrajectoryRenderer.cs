using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] int _countOfDots = 100;

    public void ShowTrajectory(Vector3 origin, Vector3 velocity)
    {
        Vector3[] points = new Vector3[_countOfDots];
        float time;

        _lineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            time = i * Time.fixedDeltaTime;

            //Если тело приземлилось, то прекращаем отрисовку.
            if (Math.Abs(2 * velocity.y / Physics.gravity.y) <= time)
            {
                _lineRenderer.positionCount = i;
                break;
            }
            
            points[i] = origin + velocity * time + Physics.gravity * time * time / 2;
        }

        _lineRenderer.SetPositions(points);
    }
}
