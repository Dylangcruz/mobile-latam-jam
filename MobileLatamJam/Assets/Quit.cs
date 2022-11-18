using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    public GameObject PM;

    public void onQuitClick()
    {
        if (Pause.isGamePaused == true)
        {
            Pause.isGamePaused = false;
            PM.SetActive(false);
            SceneManager.LoadScene(1);
        }
    }
}
