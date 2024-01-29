using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{

    void Awake()
    {
        Controller.OnSwipeEvent += context => Swipe(context);  
    }
    void Swipe(byte context)
    {
        if(context == 0)
        {
            Debug.Log("Swipe down event triggered");
        }
        else
        {
            Debug.Log("Swipe up event triggered");
        }
    }
}
