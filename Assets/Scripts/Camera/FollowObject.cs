using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private GameObject _player;
    [SerializeField] private float _smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _player.transform;
        transform.position = new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(_playerTransform.position.x, transform.position.y, _playerTransform.position.z),
            ref _currentVelocity,
            _smoothTime
        );
    }
}