using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    void OnEnable()
    {
        SwipeGame.OnGameSwipeWin += ctx => Swipe(ctx); 
    }

    void OnDisable()
    {
        SwipeGame.OnGameSwipeWin += ctx => Swipe(ctx);
    }

    void Swipe(EnemyStats stats)
    {
        Debug.Log("Player won swipe game");
    }
}
