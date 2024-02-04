using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    void OnEnable()
    {
        SwipeGame.OnGameSwipeWin += Swipe; 
    }

    void OnDisable()
    {
        SwipeGame.OnGameSwipeWin -= Swipe;
    }

    void Swipe(EnemyStats stats)
    {
        Debug.Log("Player won swipe game");
    }
}
