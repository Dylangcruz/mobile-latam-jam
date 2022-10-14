using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // used to get touch position and convert it to grid position
            touchPosition.z = 0f;

            Debug.Log("MATH " + Mathf.Round(Mathf.Abs(touchPosition.x - transform.position.x)));

            if (Mathf.Round(Mathf.Abs(touchPosition.x - transform.position.x)) >= 0.25 )
            {
                transform.position = touchPosition;

            }
            
        }
        
    }
}
