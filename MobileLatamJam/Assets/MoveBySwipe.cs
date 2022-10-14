using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBySwipe : MonoBehaviour
{

    private Vector2 startTouchPosition; //position to store where we start swipping
    private Vector2 currentPosition; // curent finger position on screen
    private Vector2 endTouchPosition; //where swipe ended

    private Vector3 Character_Position;
    private bool stopTouch = false;

     

    public float swipeRange; //target so swipe to
    public float tapRange;

    public float character_X;
    public float character_Y;

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

            Character_Position = transform.position;

    

            if(!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    //outputText.text = "Left";
                    Character_Position.x = Character_Position.x - 1;
                    stopTouch = true;
                }
                
                else if (Distance.x > swipeRange)
                {
                    Character_Position.x = Character_Position.x + 1;
                    stopTouch = true;
                }
                
                else if (Distance.y > swipeRange)
                {
                    Character_Position.y = Character_Position.y + 1;
                    stopTouch = true;
                }

                else if (Distance.y < swipeRange)
                {
                    Character_Position.y = Character_Position.y - 1;
                    stopTouch = true;
                }

                transform.position = Character_Position;

            }
        
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
        }


    }
}

