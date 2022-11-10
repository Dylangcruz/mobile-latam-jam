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
	

	public float swipeBuffer;
	private bool hasMovedThisBeat=false;
	public LayerMask whatStopsMovement;
	public LayerMask enemies;

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
			
			if (touch.phase == TouchPhase.Began )
			{
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
			}
			
			//Detects Swipe while finger is still moving on screen
			// if (touch.phase == TouchPhase.Moved)
			// {
			// 	if (!detectSwipeAfterRelease)
			// 	{
			// 		fingerDownPos = touch.position;
			// 		DetectSwipe();
			// 	}
			// }

			//Detects swipe after finger is released from screen
			if (touch.phase == TouchPhase.Ended)
			{
				if (conductorinstance.SecondsAwayFromBeat() < 0.3f)
				{	
				fingerDownPos = touch.position;
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

		if (!Attack(Vector3.down))
		{
			if(!Physics2D.OverlapCircle( Character_Position + Vector3.up,.2f, whatStopsMovement))
			{
				Character_Position += Vector3.up;
				transform.position = Character_Position;
			}
		}
	}

	void OnSwipeDown()
	{
		//Do something when swiped down
		if (!Attack(Vector3.down))
		{
			if(!Physics2D.OverlapCircle(Character_Position + Vector3.down, .2f, whatStopsMovement))
			{
				Character_Position += Vector3.down;
				transform.position = Character_Position;
			}
		}
	}

	void OnSwipeLeft()
	{
		//Do something when swiped left
		if (!Attack(Vector3.left))
		{
			if (!Physics2D.OverlapCircle(Character_Position + Vector3.left, .2f, whatStopsMovement))
			{
				Character_Position += Vector3.left;
				transform.position = Character_Position;
			}
		}
	}

	void OnSwipeRight()
	{
		//Do something when swiped right
		if (!Attack(Vector3.right))
		{
			if (!Physics2D.OverlapCircle(Character_Position + Vector3.right, .2f, whatStopsMovement))
			{
				Character_Position += Vector3.right;
				transform.position = Character_Position;
			}
		}
	}

	bool Attack(Vector3 direction)//this should later change to take into consideration weapons, range and damage
	{	Collider2D target = Physics2D.OverlapCircle(Character_Position + direction, .2f, enemies);
		float damage = 1f; //this is a dummy damage value
		if(!target)//if no enemy
		{
			return false;//dont attack 
		}else
		{
			Debug.Log("target: " + target);
			target.GetComponent<EnemyHealth>().Damage(1);
			//target.SendMessage("OnHit",damage);
			return true;
		}
	}
}