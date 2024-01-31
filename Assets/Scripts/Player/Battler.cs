using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Battler : MonoBehaviour
{
    public delegate void LoseBarttleHandler ();
    public static event LoseBarttleHandler OnLoseBattle;
    [SerializeField] private CameraZoomer _zoomer;
    [SerializeField] private float _timeToLose = 30;
    private GameObject _player;
    private EnemyStats _stats;
    private float _startTime;
    private bool _inBattle = false;

    private void Awake() 
    {
        _player = GameObject.FindGameObjectWithTag("Player");    
        _startTime = 0;
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
            _inBattle = false;
            OnLoseBattle?.Invoke();
        }    
    }

    private void StartBattle(EnemyStats stats)
    {
        _zoomer.Zoom();
        _inBattle =true;
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
