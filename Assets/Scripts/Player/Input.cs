using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private PlayerInput _input;
    [SerializeField] private MoveController _moveController;
    // Единичный вектор, задающий направление броска
    [SerializeField] private Vector3 _direction = new Vector3(1, 1, 1);
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();
        _input.Player.Throw.canceled += context => Throw();
    }

    private void Update()
    {
        SetDirection();
    }

    private void SetDirection()
    {
        Vector2 moveVector = _input.Player.Throw.ReadValue<Vector2>();
        
        if (moveVector == Vector2.zero)
        {
            return;
        }

        _direction.x = -moveVector.x;
        _direction.z = -moveVector.y;
        
        _moveController.Aim(_direction);
    }

    private void Throw()
    {
        _moveController.Throw();
    }
}
