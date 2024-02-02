using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using TMPro;

public class SwipeGame : MonoBehaviour
{
    [SerializeField] private int _sequenceLength;
    private EnemyStats _stats;
    private int[] _sequence;
    private int _currentSwipe;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private OnScreenStick _joystick;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private GameObject _swipeGameScreen;
    [SerializeField] private Image _swipeImage;
    [SerializeField] private Sprite[] _swipeSprites;


    public delegate void GameSwipe(EnemyStats stats);
    public static event GameSwipe OnGameSwipeWin;

    void Awake()
    {
        _pauseButton = GetComponentInChildren<Button>();
        _joystick = GetComponentInChildren<OnScreenStick>();
        //_pauseMenu = GetComponentInChildren<PauseMenu>();
        _swipeImage = _swipeGameScreen.GetComponentInChildren<Image>();
        Debug.Log("Aboba");
        PlayerCollider.OnTriggerWithEnemy += ctx => StartGame(ctx);
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if(other.tag == "Player")
    //     {
    //         GenerateSequence();
    //         Controller.OnSwipeEvent += SwipeMatcher;
    //     }
    // }

    void StartGame(EnemyStats stats)
    {
        _sequence = new int[_sequenceLength];
        GenerateSequence();
        ShowUI();
        Controller.OnSwipeEvent += SwipeMatcher;
        _stats = stats;
        Debug.Log("Game Started\nEnemy has this stats\n" + _stats);
    }
    void SwipeMatcher(int context)
    {
        if(_sequence[_currentSwipe] == context)
        {
            _currentSwipe ++;
            Debug.Log("Damage here");
            if(_currentSwipe == _sequenceLength)
            {
                Debug.Log("Game won\nEnemy has this stats\n" + _stats);
                HideUI();
                OnGameSwipeWin?.Invoke(_stats);
                Controller.OnSwipeEvent -= SwipeMatcher;
                return;
            }
            _swipeImage.sprite = _swipeSprites[_sequence[_currentSwipe]];
        }
        else
        {
            Debug.Log("Wrong");
            GenerateSequence();
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
        _swipeImage.sprite = _swipeSprites[_sequence[_currentSwipe]];
    }

    void ShowUI()
    {
        _pauseMenu.Pause();
        _pauseButton.gameObject.SetActive(false);
        _joystick.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(true);
        _swipeGameScreen.SetActive(true);
    }

    void HideUI()
    {
        _pauseMenu.Pause();
        _pauseButton.gameObject.SetActive(true);
        _joystick.gameObject.SetActive(true);
        _pauseMenu.gameObject.SetActive(false);
        _swipeGameScreen.SetActive(false);
    }
}
