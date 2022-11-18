using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{

    public GameObject PM;

    public void onResumeClick()
    {
        if (Pause.isGamePaused == true)
        {
            Pause.isGamePaused = false;
            PM.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
