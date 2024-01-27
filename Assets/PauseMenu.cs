using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        if(GameIsPaused)
        {
            Time.timeScale = 1.0f;
            GameIsPaused = false;
        }
        else{
            Time.timeScale = 0.0f;
            GameIsPaused = true;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
