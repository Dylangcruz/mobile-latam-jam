using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMove : MonoBehaviour
{
	private Vector2 fingerDownPos;
	private Vector2 fingerUpPos;

	public bool detectSwipeAfterRelease = false;

	public float SWIPE_THRESHOLD = 20f;

	private Vector3 Character_Position;
	private bool stopTouch = false;

	public Rigidbody2D rb;

	private GameObject ConductorObject;
	private Conductor conductorinstance;
	
	public LayerMask whatStopsMovement;

	private void Start()
    {

		Character_Position = transform.position;
		conductorinstance = GameObject.Find("Conductor").GetComponent<Conductor>();
	}
    
	// Update is called once per frame
	void Update()
	{
		Character_Position = transform.position;

		foreach (Touch touch in Input.touches)
		{

				if (touch.phase == TouchPhase.Began)
				{
					fingerUpPos = touch.position;
					fingerDownPos = touch.position;
				}

				//Detects Swipe while finger is still moving on screen
				//if (touch.phase == TouchPhase.Moved)
				//{
				//	if (!detectSwipeAfterRelease)
				//	{
				//		fingerDownPos = touch.position;
				//		DetectSwipe();
				//	}
				//}

				//Detects swipe after finger is released from screen
				if (touch.phase == TouchPhase.Ended)
				{
					fingerDownPos = touch.position;
					if (conductorinstance.canSwipe)
					{
						DetectSwipe();
					}
				}

			
		}
	}

	void DetectSwipe()
	{

		if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
		{
			Debug.Log("Vertical Swipe Detected!");
			if (fingerDownPos.y - fingerUpPos.y > 0)
			{
				OnSwipeUp();
			}
			else if (fingerDownPos.y - fingerUpPos.y < 0)
			{
				OnSwipeDown();
			}
			fingerUpPos = fingerDownPos;

		}
		else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
		{
			Debug.Log("Horizontal Swipe Detected!");
			if (fingerDownPos.x - fingerUpPos.x > 0)
			{
				OnSwipeRight();
			}
			else if (fingerDownPos.x - fingerUpPos.x < 0)
			{
				OnSwipeLeft();
			}
			fingerUpPos = fingerDownPos;

		}
		else
		{
			Debug.Log("No Swipe Detected!");
		}
	}

	float VerticalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
	}

	float HorizontalMoveValue()
	{
		return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
	}

	void OnSwipeUp()
	{
		//Do something when swiped up
		if(!Physics2D.OverlapCircle( Character_Position + Vector3.up,.2f, whatStopsMovement))
			{
			Character_Position += Vector3.up;
			transform.position = Character_Position;
			}
	}

	void OnSwipeDown()
	{
		//Do something when swiped down
		if (!Physics2D.OverlapCircle(Character_Position + Vector3.down, .2f, whatStopsMovement))
		{
			Character_Position += Vector3.down;
			transform.position = Character_Position;
		}
	}

	void OnSwipeLeft()
	{
		//Do something when swiped left
		if (!Physics2D.OverlapCircle(Character_Position + Vector3.left, .2f, whatStopsMovement))
		{
			Character_Position += Vector3.left;
			transform.position = Character_Position;
		}
	}

	void OnSwipeRight()
	{
		//Do something when swiped right
		if (!Physics2D.OverlapCircle(Character_Position + Vector3.right, .2f, whatStopsMovement))
		{
			Character_Position += Vector3.right;
			transform.position = Character_Position;
		}
	}
}