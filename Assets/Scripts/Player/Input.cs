using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private PlayerInput _input;
    //[SerializeField] private MoveController _moveController;
    InfoForPlayerStateMachine _infoForPlayerStateMachine;
    // Единичный вектор, задающий направление броска
    [SerializeField] private Vector3 _direction = new Vector3(1, 1, 1);
    [SerializeField] private TrajectoryRenderer _trajectoryRenderer;

    private Vector2 _moveVector;

    
    // private bool _isAiming;
    // public bool IsAimingCanceled
    // {
    //     get{return _isAiming;}
    //     set{_isAiming = value;}
    // }

    [Header("Swipe Detection Properties")]
    [Space]
    [SerializeField] private byte _swipeThreshold;

    // Переменные отвечающие за определение свайпа
    private Vector2 _startedPosition;
    private Vector2 _currentPosition;

    //События на свайпы
    public delegate void SwipeHandler(int direction);
    public static event SwipeHandler OnSwipeEvent;


    private void Awake()
    {
        _input = new PlayerInput();
        TryGetComponent<InfoForPlayerStateMachine>(out _infoForPlayerStateMachine);
    }

    void OnEnable()
    {
        _input.Enable();
        _input.Player.Throw.canceled += context => Throw();
        _input.Player.TouchPress.started += context => {_startedPosition = _input.Player.TouchPosition.ReadValue<Vector2>();};
        _input.Player.TouchPosition.performed += context => { _currentPosition = _input.Player.TouchPosition.ReadValue<Vector2>();};
        _input.Player.TouchPress.canceled += context => SwipeDetection();
    }

    void OnDisable()
    {
        _input.Player.Throw.canceled -= context => Throw();
        _input.Player.TouchPress.started -= context => {_startedPosition = _input.Player.TouchPosition.ReadValue<Vector2>();};
        _input.Player.TouchPosition.performed -= context => { _currentPosition = _input.Player.TouchPosition.ReadValue<Vector2>();};
        _input.Player.TouchPress.canceled -= context => SwipeDetection();
         _input.Disable();
    }

    private void Update()
    {
        SetDirection();
    }

    private void SetDirection()
    {
        _moveVector = _input.Player.Throw.ReadValue<Vector2>();
        _infoForPlayerStateMachine.MoveVector = _moveVector;
        Debug.Log($"_moveVector {_moveVector}");
        Debug.Log($"_infoForPlayerStateMachine.MoveVector {_infoForPlayerStateMachine.MoveVector}");
    }
    private void Throw()
    {
        _infoForPlayerStateMachine.IsAimingCanceled = true;
    }

    private void SwipeDetection()
    {
        Vector2 delta = _currentPosition - _startedPosition;
        
        if(delta.magnitude < _swipeThreshold)
        {
            Debug.Log("Touched no swipe");
            return;
        }

        if((Mathf.Abs(delta.x) < Mathf.Abs(delta.y)))
        {
            if (delta.y > 0)
            {
                OnSwipeEvent?.Invoke(1);
            }
            else
            {
                OnSwipeEvent?.Invoke(0);
            }
        }

    }
}
