using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeGame : MonoBehaviour
{
    [SerializeField] private int _sequenceLength;
    private int[] _sequence;
    private int _currentSwipe;

    public delegate void GameSwipe();
    public static event GameSwipe OnGameSwipeWin;

    void Awake()
    {
        _sequence = new int[_sequenceLength];
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GenerateSequence();
            Controller.OnSwipeEvent += SwipeMatcher;
        }
    }


    void SwipeMatcher(int context)
    {
        if(_sequence[_currentSwipe] == context)
        {
            _currentSwipe ++;
            Debug.Log("Correct " + _currentSwipe);
            if(_currentSwipe == _sequenceLength)
            {
                OnGameSwipeWin?.Invoke();
                Controller.OnSwipeEvent -= SwipeMatcher;
            }
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
    }
}
