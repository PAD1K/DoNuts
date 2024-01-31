using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShower : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;

    public void ShowTarget(Vector3 position)
    {
        _spriteRenderer.enabled = true;
        transform.position = position;
    }

    public void HideTarget()
    {
        _spriteRenderer.enabled = false;
    }

    private void Awake() 
    {
        HideTarget();
    }

    void OnEnable()
    {
        MoveController.OnLandEvent += HideTarget;
    }

    void OnDisable()
    {
        MoveController.OnLandEvent -= HideTarget;
    }
}
