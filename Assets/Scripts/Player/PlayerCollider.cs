using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public delegate void CollideHandler (EnemyStats stats);
    public static event CollideHandler OnTriggerWithEnemy;


    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag != "Enemy")
        {
            return;
        }
        
        EnemyStats stats = new EnemyStats();
        other.gameObject.TryGetComponent<EnemyStats>(out stats);
        OnTriggerWithEnemy?.Invoke(stats);
    }
}
