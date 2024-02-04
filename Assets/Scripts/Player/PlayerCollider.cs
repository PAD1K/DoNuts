using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public delegate void CollideHandler (EnemyStats stats);
    public static event CollideHandler OnTriggerWithEnemy;
    private EnemyStats _stats;


    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag != "Enemy")
        {
            return;
        }
        other.gameObject.TryGetComponent<EnemyStats>(out _stats);
        OnTriggerWithEnemy?.Invoke(_stats);
    }
}