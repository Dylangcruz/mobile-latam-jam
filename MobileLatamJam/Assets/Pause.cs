using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Pause : MonoBehaviour
{
    public static bool isGamePaused;
    

    public void OnPauseGame()
    {
        isGamePaused = false;
        if(isGamePaused == false)
        {
            Time.timeScale = 0;
            print("Hi!");
            Debug.Log("Hi!");
            Time.timeScale = 1;
            isGamePaused = true;
        }

        else
        {
            Time.timeScale = 1;
        }

    }

}
