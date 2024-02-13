using UnityEngine;

public class SwipeGame : MonoBehaviour
{
    [SerializeField] private SwipeUIController _uiController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private int _sequenceLength;
    [SerializeField] private float _timeToLose;
    [SerializeField] private uint _pointDelta;
    private EnemyStats _enemyStats;
    private int[] _sequence;
    private int _currentSwipe;
    private PlayerStats _playerStats;
    private float _startTime;
    [SerializeField] private float _currentTime;
    private bool _inBattle = false;

    void Awake()
    {
        _startTime = 0;
        gameObject.TryGetComponent<PlayerStats>(out _playerStats);
    }

    void Update()
    {
        if (!_inBattle)
        {
            return;
        }
        
        Debug.Log($"_startTime = {_startTime}");
        Debug.Log($"Time.time = {Time.fixedTime}");
        Debug.Log($"_startTime + _timeToLose = {_startTime + _timeToLose}");
        _currentTime += Time.fixedDeltaTime;
        if (_currentTime > _startTime + _timeToLose)
        {
            _uiController.HideUI();
            LoseBattle();
        }
    }

    void StartGame()
    {
        _sequence = new int[_sequenceLength];
        GenerateSequence();
        _uiController.ShowUI();
        InputController.OnSwipeEvent += SwipeMatcher;
        Debug.Log("Game Started");
    }
    void SwipeMatcher(int context)
    {
        if(_sequence[_currentSwipe] == context)
        {
            _currentSwipe ++;
            Debug.Log("Damage here");
            if(_currentSwipe == _sequenceLength)
            {
                _uiController.HideUI();
                WinBattle();
                return;
            }
            _uiController.SetUISwipeImage(_sequence[_currentSwipe]);
        }
        else
        {
            Debug.Log("Wrong");
            _uiController.HideUI();
            LoseBattle();
        }
    }

    void GenerateSequence()
    {
        Debug.Log("Restarting");
        _currentSwipe = 0;
        for(int i = 0; i < _sequenceLength; i++)
        {
            _sequence[i] = Random.Range(0,2);
            Debug.Log(_sequence[i]);
        }
        _uiController.SetUISwipeImage(_sequence[_currentSwipe]);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag != "Enemy")
        {
            return;
        }
        _startTime = Time.fixedTime;
        _currentTime = _startTime;
        other.gameObject.TryGetComponent<EnemyStats>(out _enemyStats);
        StartBattle();
    }

    private void StartBattle()
    {
        _inBattle = true;
        _cameraController.Zoom();
        StartGame();
    }

    private void WinBattle()
    {
        InputController.OnSwipeEvent -= SwipeMatcher;
        _cameraController.Zoom();
        _playerStats.IncreasePoints(_pointDelta);
        _inBattle = false;
    }

    private void LoseBattle()
    {
        Debug.Log("Swipe game lost");
        InputController.OnSwipeEvent -= SwipeMatcher;
        _cameraController.Zoom();
        _playerStats.TakeDamage(_enemyStats);
        _inBattle = false;
    }
}
