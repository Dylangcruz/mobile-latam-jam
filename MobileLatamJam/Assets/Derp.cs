using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derp : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Pause.isGamePaused==false)
        {
            print(10000000000000000);
        }

        else
        {
            print("derp");
        }
        
        
    }
}
