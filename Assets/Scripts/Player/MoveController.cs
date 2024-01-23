using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _force = 400;
    [SerializeField] private float _angle = 45f;
    private Rigidbody _rigidbody;

    private void Awake() 
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public void Throw(Vector3 direction)
    {
        direction.x *= Mathf.Cos(_angle * Mathf.Deg2Rad);
        direction.y *= Mathf.Sin(_angle * Mathf.Deg2Rad);
        _rigidbody.AddForce(direction * _force, ForceMode.Acceleration);
    }
}
