using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Battler : MonoBehaviour
{
    [SerializeField] private CameraZoomer _zoomer;
    private GameObject _player;
    private EnemyStats _stats;
    
    private void Awake() 
    {
        _player = GameObject.FindGameObjectWithTag("Player");    
    }

    private void StartBattle(EnemyStats stats)
    {
        _zoomer.Zoom();
    }

    private void OnEnable() 
    {
        PlayerCollider.OnTriggerWithEnemy += StartBattle;
    }

    private void OnDisable() 
    {
        PlayerCollider.OnTriggerWithEnemy += StartBattle;
    }
}
