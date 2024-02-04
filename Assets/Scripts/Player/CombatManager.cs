using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public delegate void LoseBarttleHandler (EnemyStats _enemyStats);
    public static event LoseBarttleHandler OnLoseBattle;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private float _timeToLose = 10;
    [SerializeField] private uint _pointDelta = 1;

    private GameObject _player;
    private EnemyStats _enemyStats;
    private PlayerStats _playerStats;
    private float _startTime;
    private bool _inBattle = false;

    private void Awake() 
    {
        _player = GameObject.FindGameObjectWithTag("Player");    
        _startTime = 0;
        gameObject.TryGetComponent<PlayerStats>(out _playerStats);
    }

    private void Update() 
    {
        if (!_inBattle)
        {
            return;
        }

        if (_startTime == 0)
        {
            _startTime = Time.time;
        }

        if (Time.time > _startTime + _timeToLose)
        {
            LoseBattle();
        }    
    }

    private void StartBattle(EnemyStats stats)
    {
        _enemyStats = stats;
        _cameraController.Zoom();
        _inBattle =true;
    }

    private void WinBattle(EnemyStats stats)
    {
        _cameraController.Zoom();
        _playerStats.IncreasePoints(_pointDelta);
    }

    private void LoseBattle()
    {
        OnLoseBattle?.Invoke(_enemyStats);
        _inBattle = false;
    }

    private void OnEnable() 
    {
        PlayerCollider.OnTriggerWithEnemy += StartBattle;
        SwipeGame.OnGameSwipeWin += WinBattle;
    }

    private void OnDisable() 
    {
        PlayerCollider.OnTriggerWithEnemy -= StartBattle;
        SwipeGame.OnGameSwipeWin -= WinBattle;
    }
}
