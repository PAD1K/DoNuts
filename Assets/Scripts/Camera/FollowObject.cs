using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;
    
    private void Awake()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(target.position.x, transform.position.y, target.position.z),
            ref _currentVelocity,
            smoothTime
        );
    }
}