using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private uint _health;
    private uint _points;
    private MoveController _moveController;

    public void IncreasePoints(uint increaseValue)
    {
        _points += increaseValue;
    }
    
    public void TakeDamage(EnemyStats _enemyStats)
    {
        // TODO: получение урона от врага
        // _health -= damage;
        Stun();
    }

    public void Stun()
    {
        _moveController.Stun();
    }

    private void Awake() 
    {
        _points = 0;
        //CombatManager.OnLoseBattle += TakeDamage;
        gameObject.TryGetComponent<MoveController>(out _moveController);
    }
}
