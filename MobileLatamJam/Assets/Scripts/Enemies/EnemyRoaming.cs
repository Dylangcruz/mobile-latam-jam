using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRoaming : MonoBehaviour
{
    private enum State
    {
        Roaming,
        Chasing, 
    }
    private State state;
    private Vector3 startPosition;
    private Vector3 roamPosition;
    
    public Transform target;
    public Vector3 endpoint;
    public int counter = 0; 
    public float nextWaipointDistance = 3f;

    private GameObject ConductorObject;
	private Conductor conductorinstance;

    private int currentBeat=-1;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    bool moved = false;


    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    { 
        state = State.Roaming;

        endpoint = RandomPosition(startPosition);

        conductorinstance = GameObject.Find("Conductor").GetComponent<Conductor>();
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath",0f,.5f);
    }

    Vector3 RandomPosition(Vector3 initialPosition)
    {
        var newtarget = new Vector3 ( Mathf.Floor(Random.Range(initialPosition.x -3,initialPosition.x + 3)) + .5f,
                                        Mathf.Floor(Random.Range(initialPosition.y -3,initialPosition.y +3))+ .5f,-1); 

        return newtarget;
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(transform.position,endpoint, OnPathComplete);
    
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

    void Move()
    {
        Vector3 nextPosition =  path.vectorPath[1] + Vector3.back;//where we wanna move
        switch(state)
        {
            default:
            case State.Roaming:
                if(transform.position==nextPosition)//if got to the roaming spot
                {
                   endpoint = RandomPosition(startPosition);//find new spot
                }else
                {
                    transform.position = nextPosition;//otherwise keep going to the spot
                }


                if(Vector3.Distance(transform.position, target.position) < 5)//if target in range
                {
                    state = State.Chasing;//begin chase
                    endpoint = target.position;
                }
                break;

            case State.Chasing:
                endpoint = target.position;
                if(target.position != nextPosition)//if the target is not in the next spot
                {
                    transform.position = nextPosition;//MOVE

                }else // o sea, target is in the next spot
                {
                //ATTACK ANIMATION
                //DEPLETE HEALTH  
                }


                if(Vector3.Distance(transform.position, target.position) > 7)//if target got away from range
                {
                    endpoint = RandomPosition(startPosition);
                    state = State.Roaming; //go back to roaming
                }
                break;

        }
    }


    // Update is called once per frame
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
       
        if (conductorinstance.songPositionInBeats == currentBeat +2 )
        {

            currentBeat = conductorinstance.songPositionInBeats;

            Move();

        }
    }
}
