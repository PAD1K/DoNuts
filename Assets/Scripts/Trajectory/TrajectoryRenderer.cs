using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] int _countOfDots = 100;
    [SerializeField] TargetSprite _targetSprite; 
    public void ShowTrajectory(Vector3 origin, Vector3 velocity)
    {
        Vector3[] points = new Vector3[_countOfDots];
        float time;

        _lineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            time = i * Time.fixedDeltaTime;
            points[i] = origin + velocity * time + Physics.gravity * time * time / 2;

            //Прекращаем отрисовку в точке, где должно приземлиться тело.
            if (Math.Abs(2 * velocity.y / Physics.gravity.y) <= time)
            {
                _lineRenderer.positionCount = i;
                _targetSprite.ShowSprite(points[i]);
                break;
            }
        }

        _lineRenderer.SetPositions(points);
    }
}
