using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] int _countOfDots;
    [SerializeField] TargetShower _targetSprite; 

    public void ShowTrajectory(Vector3 origin, Vector3 velocity)
    {
        if (velocity.x ==0 && velocity.z == 0)
        {
            return;
        }
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
                _targetSprite.ShowTarget(points[i]);
                break;
            }
        }

        _lineRenderer.SetPositions(points);
    }

    private void Awake() 
    {
        TryGetComponent<LineRenderer>(out _lineRenderer);
        HideTrajectory();
    }

    public void HideTrajectory()
    {
        _lineRenderer.positionCount = 0;
        _targetSprite.HideTarget();
    }
}
