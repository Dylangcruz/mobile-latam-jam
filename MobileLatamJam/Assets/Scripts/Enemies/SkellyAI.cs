using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
  
public class SkellyAI : MonoBehaviour
{

    public GameObject targetObject;
    public GameObject arrowPrefab;
    
    private Transform target;
    private  PlayerHealth targetHealth;
   
    private int counter = 0; 
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

    private string enemyName;  //Skelly_Melee

	private string ENEMY_IDLE;
	private string ENEMY_MOVE;
	private string ENEMY_ATTACK;

    public enum Form
    {
        Imp,
        Skelly, 
    } 
    public enum Type
    {
        Melee,
        Ranged, 
    }
    

    public Form enemyForm;
    public Type enemyType;
    
    public int range = 3;
    

    private Vector3 spawnPoint;
    private Quaternion arrowRotation;
    public LayerMask enemies;


    Seeker seeker;

	//=====================================================
	// Start is called before the first frame update
    //=====================================================
    void Start()
    { 
        conductorinstance = GameObject.Find("Conductor").GetComponent<Conductor>();

        enemyName = ((enemyForm == Form.Imp)? "Imp" : "Skelly") 
                  + ((enemyType == Type.Melee)? "_Melee" : "_Ranged");

        ENEMY_IDLE = enemyName + "_Idle_";
        ENEMY_MOVE = enemyName + "_Move_";
        ENEMY_ATTACK = enemyName + "_Attack_";
        

        anim.speed = conductorinstance.songBpm/60;//animations are made calculated to 1 sec 1/60 is to 1 min * bpm = per beat....speed = beat

        Debug.Log("Name: "+ enemyName);
        
        target = targetObject.transform;
        targetHealth = targetObject.GetComponent<PlayerHealth>();

        
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath",0f,conductorinstance.secPerBeat);
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
            if(Vector3.Distance(transform.position, target.position) < range + 3)//if target got away is in range
            {
                Move();//this also attacks
            }
            
        }
    }
    

    void UpdatePath()
    {
        //path.active.Scan();
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
        Vector3 nextPosition =  path.vectorPath[1] + Vector3.back;//where we wanna move+
        aniDirection = ChangeDirection(target.position);

        if(!Physics2D.OverlapCircle( nextPosition,.2f, enemies))
        {
            switch(enemyType)
            {
                default:
                case Type.Melee:
                    if(target.position != nextPosition)//if the target is not in the next spot
                    { 
                    //change direction
                    aniDirection = ChangeDirection(nextPosition);
                    //MOVE ANIMATION
                    ChangeAnimationState(ENEMY_MOVE);
                    //MOVEMENT
                    //transform.position = nextPosition;//should change this to MoveTowards()
                    transform.position = Vector3.MoveTowards(transform.position, 
													nextPosition, 
													3/(conductorinstance.secPerBeat * 60));
                
            
                    }else // osea, target is here
                    {
                        //ATTACK ANIMATION
                        ChangeAnimationState(ENEMY_ATTACK);
                        //DEPLETE HEALTH 
                        targetHealth.Damage(1);
                        
                    }
                    break;
                case Type.Ranged:

                    if( transform.position.x != target.position.x && transform.position.y != target.position.y 
                    || Mathf.Abs(transform.position.y - target.position.y) > range && Mathf.Abs(transform.position.x - target.position.x) > range)//if the target is not in range
                        { 
                        //change direction
                        aniDirection = ChangeDirection(nextPosition);
                        //MOVE ANIMATION
                        ChangeAnimationState(ENEMY_MOVE);
                        //MOVEMENT
                        //transform.position = nextPosition;//should change this to MoveTowards()
                        transform.position = Vector3.MoveTowards(transform.position, 
													nextPosition, 
													3/(conductorinstance.secPerBeat * 60));
                
                    }else // osea, target is here
                    {
                        //ATTACK ANIMATION
                        ChangeAnimationState(ENEMY_ATTACK);

                        //SPAWN ARROW            
                        var arrow = Instantiate(arrowPrefab, transform.position + spawnPoint,arrowRotation);            
                        arrow.GetComponent<Arrow>().Initialize(aniDirection, range);
                        
                        
                    }
                    break;
            }
        }//else theres another enemy in my way


    }

	//=====================================================
    // ChangeAnimationState is a mini animation manager
    //=====================================================
    void ChangeAnimationState(string newAnimation)
    {
		newAnimation = newAnimation + aniDirection;//aniDirection is imperative
        if (currentAnimaton == newAnimation) return;
        Debug.Log("play: "+newAnimation);
        anim.Play(newAnimation,0, conductorinstance.songPositionInBeatsUnfloored-conductorinstance.songPositionInBeats);
        currentAnimaton = newAnimation;
    }

	//=====================================================
    // ChangeDirection checks whre the skelly is looking
    //=====================================================
    string ChangeDirection(Vector3 nextPosition)
    {
        Vector3 directionVector = nextPosition - transform.position;

        Debug.Log(enemyName +"Direction Vector: "+ directionVector );
        string direction = aniDirection;
        if(directionVector.x == 0 && directionVector.y == 0)
        { 
            return direction;
        }else if(directionVector.x != 0 && directionVector.y == 0)
        {
            if(directionVector.x > 0)
            {
                direction = "Right";
                spawnPoint = new Vector3 (1f, 0, 0 );
                arrowRotation = Quaternion.Euler(0,0,0);
            }else 
            {
                direction = "Left";
                spawnPoint = new Vector3 (-1f, 0, 0 );
                arrowRotation = Quaternion.Euler(0,180,0);

            }
        }else
        {
            if(directionVector.y > 0)
            {
                direction = "Up";
                spawnPoint = new Vector3 (0 , 1f, 0 );
                arrowRotation = Quaternion.Euler(0,0,90);
            }else 
            {
                direction = "Down";
                spawnPoint = new Vector3 (0 , -1f, 0 );
                arrowRotation = Quaternion.Euler(0,0,270);
            }   
        }
        
        Debug.Log(enemyName +" is Looking: " + direction);
        return direction;
        
    }

}
