using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _distanceFromPlayer = 2;
    private GameObject _player;
    private Vector3 _currentVelocity = Vector3.zero;
    private bool _isZoom = false;
    
    public void Zoom()
    {
        if (!_isZoom)
        {
            _isZoom = true;
        }
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(_playerTransform.position.x, _playerTransform.position.y + _distanceFromPlayer, _playerTransform.position.z),
            ref _currentVelocity,
            _smoothTime
        );
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _player.transform;
    }

    private void Update() {
        if (_isZoom)
        {
            Zoom();
        }
    }
}