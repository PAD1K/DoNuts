using UnityEngine;

public class MoveController : MonoBehaviour
{
    private bool _isStun = false;
    private float _startStunTime = 0;

    public void Stun()
    {
        if (_isStun)
        {
            return;
        }
        
        _isStun = true;
        _startStunTime = Time.time;
    }

    
}
