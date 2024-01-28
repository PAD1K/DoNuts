using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer; // Ссылка на компонент Sprite Renderer

    public void ShowSprite(Vector3 position)
    {
        _spriteRenderer.enabled = true;
        transform.position = position;
    }

    public void HideSprite()
    {
        _spriteRenderer.enabled = false;
    }

    private void Awake() 
    {
        HideSprite();
        MoveController.OnLandEvent += HideSprite;
    }
}
