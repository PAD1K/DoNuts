using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private PlayerInput _input;
    [SerializeField] MoveController _moveController;
    // Единичный вектор, задающий направление броска
    [SerializeField] Vector3 _direction = new Vector3(1, 1, 1);
    [SerializeField] TrajectoryRenderer _trajectoryRenderer;

    void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();
        _input.Player.Throw.performed += context => Throw();
    }

    private void Update()
    {
        SetDirection();
    }

    private void SetDirection()
    {
        Vector2 moveVector = _input.Player.Move.ReadValue<Vector2>();
        _direction.x = moveVector.x;
        _direction.z = moveVector.y;
    }

    private void Throw()
    {
        _moveController.Throw(_direction);
    }
}
