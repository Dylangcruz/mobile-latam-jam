using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public GameObject targetObject;
    private Transform target;
    private  PlayerHealth targetHealth;
    public int counter = 0; 
    public float nextWaipointDistance = 3f;

    private GameObject ConductorObject;
	private Conductor conductorinstance;

    private int currentBeat=-1;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool moved = false;
    
    //Animator and Animation States
	public Animator anim;
	private string currentAnimaton; //this should be a State + aniDirection
	private string aniDirection = "Down";

    [SerializeField] private string enemyName;  //Skelly_Melee

	private string ENEMY_IDLE;
	private string ENEMY_MOVE;
	private string ENEMY_ATTACK;


    Seeker seeker;

	//=====================================================
	// Start is called before the first frame update
    //=====================================================
    void Start()
    { 
        ENEMY_IDLE = enemyName + "_Idle_";
        ENEMY_MOVE = enemyName + "_Move_";
        ENEMY_ATTACK = enemyName + "_Attack_";

        target = targetObject.transform;
        targetHealth = targetObject.GetComponent<PlayerHealth>();

        conductorinstance = GameObject.Find("Conductor").GetComponent<Conductor>();
        
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath",0f,.5f);
    }


	//=====================================================
    // Update is called once per frame
    //=====================================================

    void Update()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

       counter = path.vectorPath.Count;
       
        if (conductorinstance.songPositionInBeats != currentBeat)
        {
            currentBeat = conductorinstance.songPositionInBeats;//update which beat we're on
            UpdatePath();//update the path enemy is taking

            Move();//this also attacks
        }
    }
    

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(transform.position,target.position, OnPathComplete);
        }
    }


    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
	//=====================================================
    // Move() Checks the next position and either moves 
    //          towards it or attacks it.
    //=====================================================
    void Move()
    {
        Vector3 nextPosition =  path.vectorPath[1] + Vector3.back;//where we wanna move
        

        if(target.position != nextPosition)//if the target is not in the next spot
        {   //change direction
            aniDirection = ChangeDirection(nextPosition);
            //MOVE ANIMATION
            ChangeAnimationState(ENEMY_MOVE);
            //MOVEMENT
            transform.position = nextPosition;//should change this to MoveTowards()
        
        }else // osea, target is here
        {
          aniDirection = ChangeDirection(target.position);
          //ATTACK ANIMATION
          ChangeAnimationState(ENEMY_ATTACK);
          //DEPLETE HEALTH 
          targetHealth.Damage(1);
        }
    }

	//=====================================================
    // ChangeAnimationState is a mini animation manager
    //=====================================================
    void ChangeAnimationState(string newAnimation)
    {
		newAnimation = newAnimation + aniDirection;
        if (currentAnimaton == newAnimation) return;

        anim.Play(newAnimation);//aniDirection is imperative
        currentAnimaton = newAnimation;
    }
	//=====================================================
    // ChangeDirection checks whre the skelly is looking
    //=====================================================
    string ChangeDirection(Vector3 nextPosition)
    {
        Vector3 directionVector = nextPosition - transform.position;

        Debug.Log(enemyName +" Direction Vector: "+ directionVector );
        string direction = aniDirection;
        if(directionVector.x == 0 && directionVector.y == 0)
        { 
            return direction;
        }else if(directionVector.x != 0 && directionVector.y == 0)
        {
            if(directionVector.x >0)
            {
                direction = "Right";
            }else 
            {
                direction = "Left";
            }
        }else
        {
            if(directionVector.y >0)
            {
                direction = "Up";
            }else 
            {
                direction = "Down";
            }   
        }
        
        Debug.Log(enemyName +" is Looking: " + direction);
        return direction;
        
    }

}
