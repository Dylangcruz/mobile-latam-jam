using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwippingPlayer : MonoBehaviour
{

    public Text outputText;

    private Vector2 startTouchPosition; //position to store where we start swipping
    private Vector2 currentPosition; // curent finger position on screen
    private Vector2 endTouchPosition; //where swipe ended
    private bool stopTouch = false;

    public float swipeRange; //target so swipe to
    public float tapRange;

    // Update is called once per frame
    void Update()
    {
        Swipe();
        
    }

    public void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position; // first touch of screen stored

        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

            if(!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    outputText.text = "Left";
                    stopTouch = true;
                }
                
                else if (Distance.x > swipeRange)
                {
                    outputText.text = "Right";
                    stopTouch = true;
                }
                
                else if (Distance.y > swipeRange)
                {
                    outputText.text = "Up";
                    stopTouch = true;
                }

                else if (Distance.y < swipeRange)
                {
                    outputText.text = "Down";
                    stopTouch = true;
                }

            }
        
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;

            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
            {
                outputText.text = "Tap";
            }

        }

    }
}
