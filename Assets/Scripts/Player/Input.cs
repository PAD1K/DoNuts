using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private PlayerInput _input;
    [SerializeField] MoveController _moveController;

    void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();
        _input.Player.Throw.performed += context => Throw();
    }

    private void Throw()
    {
        _moveController.Throw(new Vector3(1, 1, 0));
    }
}
