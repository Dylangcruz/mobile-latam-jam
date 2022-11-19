using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    private bool stopTouch = false;

    // Update is called once per frame
    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Ended)
            {
                print("100000");
                SceneManager.LoadScene(1);
            }
        }
        
    }
}
