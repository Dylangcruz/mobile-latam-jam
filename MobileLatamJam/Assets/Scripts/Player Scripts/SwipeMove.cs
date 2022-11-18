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

	public static Vector3 Character_Position;
	
	
	//Animator and Animation States
	public Animator anim;
	private string currentAnimaton; //this should be a State + aniDirection
	private string aniDirection = "Down" ;
	const string PLAYER_IDLE = "Player_Idle_";
	const string PLAYER_MOVE = "Player_Move_";
	const string PLAYER_ATTACK = "Player_Attack_";

	//reference to conductor
	private GameObject ConductorObject;
	private Conductor conductorinstance;

	//layers to check if theres enemies or walls
	public LayerMask whatStopsMovement;
	public LayerMask enemies;

	private AudioSource soundFX;

	//=====================================================
	// Start is called before the first frame update
    //=====================================================
	private void Start()
    {
		Character_Position = transform.position;
		conductorinstance = GameObject.Find("Conductor").GetComponent<Conductor>();
		soundFX = GetComponent<AudioSource>();
		anim.speed = conductorinstance.songBpm/60;//animations are made calculated to 1 sec 1/60 is to 1 min * bpm = per beat....speed = beat
	}

	//=====================================================
    // Update is called once per frame
    //=====================================================
	void Update()
	{///
		if(transform.position == Character_Position)
		{
			anim.SetBool("isMoving", false);
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
					soundFX.Play();
					
					}
				}
			}
		}else
		{

			transform.position = Vector3.MoveTowards(transform.position, 
													Character_Position, 
													3/(conductorinstance.secPerBeat * 60)); // its 3// cause i want to move in a 3rd of a beat
													//(from, to , how much to move per frame)

			//Character_Position = transform.position;
			
		}
			
	}

	//=====================================================
	// DetectSwipe recognizes the direction of the swipe
	//			   and runs the proper code for it. 
    //=====================================================
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
			Debug.Log("No Swipe Detected");
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
	
	//=====================================================
	// OnSwipe___ changes the direction of the animation,
	//		 checks if theres an enemy to attack there 
	//		 then if there isn't it tries to move there.
    //=====================================================
	void OnSwipeUp()
	{
		aniDirection = "Up";
		//Do something when swiped up

		if (!Attack(Vector3.up))
		{
			if(!Physics2D.OverlapCircle( Character_Position + Vector3.up,.2f, whatStopsMovement))
			{
				anim.SetBool("isMoving", true);
				ChangeAnimationState(PLAYER_MOVE);

				Character_Position += Vector3.up;

				//transform.position = Character_Position;
			}
		}
	}

	void OnSwipeDown()
	{
		aniDirection = "Down";
		//Do something when swiped down
		if (!Attack(Vector3.down))
		{
			if(!Physics2D.OverlapCircle(Character_Position + Vector3.down, .2f, whatStopsMovement))
			{
				anim.SetBool("isMoving", true);
				ChangeAnimationState(PLAYER_MOVE);
				Character_Position += Vector3.down;
				//transform.position = Character_Position;
			}
		}
	}

	void OnSwipeLeft()
	{
		aniDirection = "Left";

		//Do something when swiped left
		if (!Attack(Vector3.left))
		{
			if (!Physics2D.OverlapCircle(Character_Position + Vector3.left, .2f, whatStopsMovement))
			{	
				anim.SetBool("isMoving", true);
				ChangeAnimationState(PLAYER_MOVE);
				Character_Position += Vector3.left;
				//transform.position = Character_Position;
			}
		}
	}

	void OnSwipeRight()
	{
		aniDirection = "Right";
		//Do something when swiped right
		if (!Attack(Vector3.right))
		{
			if (!Physics2D.OverlapCircle(Character_Position + Vector3.right, .2f, whatStopsMovement))
			{
				anim.SetBool("isMoving", true);
				ChangeAnimationState(PLAYER_MOVE);
				Character_Position += Vector3.right;
				//transform.position = Character_Position;	
					
			}
		}
	}

	//=====================================================
	// Attack tries to attack a position
	//			returns false if nothing there
    //=====================================================
	bool Attack(Vector3 direction)//this should later change to take into consideration weapons, range and damage
	{	Collider2D target = Physics2D.OverlapCircle(Character_Position + direction, .2f, enemies);
		float damage = 1f; //this is a dummy damage value
		if(!target)//if no enemy
		{
			return false;//dont attack 
		}else
		{
			anim.SetBool("isAttacking", true);
			ChangeAnimationState(PLAYER_ATTACK);

			Debug.Log("target: " + target);
			target.GetComponent<EnemyHealth>().Damage(1);
			//target.SendMessage("OnHit",damage);
			return true;
		}
	}


	//=====================================================
    // ChangeAnimationState is a mini animation manager
    //=====================================================
    void ChangeAnimationState(string newAnimation)
    {
		newAnimation = newAnimation + aniDirection;
        if (currentAnimaton == newAnimation) return;

        anim.Play(newAnimation,0, conductorinstance.songPositionInBeatsUnfloored-conductorinstance.songPositionInBeats);//aniDirection is imperative
        currentAnimaton = newAnimation;
    }
}