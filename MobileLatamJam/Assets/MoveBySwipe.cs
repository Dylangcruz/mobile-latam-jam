using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBySwipe : MonoBehaviour
{

    private Vector2 startTouchPosition; //position to store where we start swipping
    private Vector2 currentPosition; // curent finger position on screen
    private Vector2 endTouchPosition; //where swipe ended
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

            Vector3 Character_Position = Camera.main.ScreenToWorldPoint(Character_Position);

            character_X = transform.position.x;
            character_Y = transform.position.y;

    

            if(!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    //outputText.text = "Left";
                    transform.position.x = (character_X- 1);
                    stopTouch = true;
                }
                
                else if (Distance.x > swipeRange)
                {
                    transform.position.x = (character_X + 1);;
                    stopTouch = true;
                }
                
                else if (Distance.y > swipeRange)
                {
                    transform.position.y = (character_Y + 1);
                    stopTouch = true;
                }

                else if (Distance.y < swipeRange)
                {
                    transform.position.y = (character_Y - 1);
                    stopTouch = true;
                }

            }
        
        }


    }
}

