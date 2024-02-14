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

    public Vector3[] GenerateArc(Vector3 startPoint, Vector3 endPoint, int countOfDots)
    {
        Vector3[] arcPoints = new Vector3[countOfDots];
        float h = (endPoint - startPoint).magnitude / 2; // Высота дуги
        Vector3 midPoint = (startPoint + endPoint) / 2; // Середина дуги

        for (int i = 0; i < countOfDots; i++)
        {
            float t = (float)i / (countOfDots - 1); // Параметр t
            Vector3 point = Vector3.Lerp(startPoint, endPoint, t); // Точка на линии между A и B
            float y = h - 4 * h * (float)Math.Pow((point.x - midPoint.x) / (endPoint.x - startPoint.x), 2); // Вычисление y-координаты
            arcPoints[i] = new Vector3(point.x, y, point.z);
        }

        return arcPoints;
    }

    public void ShowTrajectory(Vector3 origin, Vector3 velocity, float angle)
    {
        if (velocity.x ==0 && velocity.z == 0)
        {
            return;
        }

        // Вычисление времени полета
        float time = (2 * velocity.magnitude * Mathf.Sin(angle * Mathf.Deg2Rad)) / MathF.Abs(Physics.gravity.y);

        Vector3 finalPosition = origin + velocity * time - 0.5f * Vector3.up * MathF.Abs(Physics.gravity.y) * time * time;
        finalPosition.y = origin.y;

        _lineRenderer.positionCount = _countOfDots;
        _lineRenderer.SetPositions(GenerateArc(origin, finalPosition, _countOfDots));
        
        _targetSprite.ShowTarget(finalPosition);
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
