using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private float _zoomDistance = 8;
    [SerializeField] private float _maxHeight = 20;
    private float _currentHeight;
    private GameObject _player;
    private Vector3 _currentVelocity = Vector3.zero;
    private bool _isZoom = false;
    
    /// <summary>
    /// Метод меняет состояние между зумом и анзумом.
    /// </summary>
    public void Zoom()
    {
        _isZoom = !_isZoom;
    }

    public void FollowPlayer()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(_playerTransform.position.x, _currentHeight, _playerTransform.position.z),
            ref _currentVelocity,
            _smoothTime,
            Mathf.Infinity,
            Time.fixedDeltaTime
        );
    }

    private void Awake()
    {
        _currentHeight = _maxHeight;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _player.transform;
        transform.position = new Vector3(_playerTransform.position.x, _currentHeight, _playerTransform.position.z);
    }

    private void CalculateHeight()
    {
        if (_isZoom)
        {
            _currentHeight = _playerTransform.position.y + _zoomDistance;
        }
        else
        {
            _isZoom = false;
            _currentHeight = _maxHeight;
        }
    }

    private void LateUpdate()
    {
        CalculateHeight();
        FollowPlayer();
    }
}