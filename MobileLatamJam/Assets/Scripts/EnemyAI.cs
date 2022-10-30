using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;

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
        conductorinstance = GameObject.Find("Conductor").GetComponent<Conductor>();
        
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath",0f,.5f);
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
    // Update is called once per frame
    void Update()
    {
        if (path ==null)
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
            currentBeat = conductorinstance.songPositionInBeats;

            transform.position = path.vectorPath[1] + Vector3.back;

        }
    //    if (conductorinstance.onBeat)
    //     { if (moved == false)
    //         {
    //             transform.position = path.vectorPath[1] + Vector3.back;
    //             moved = true;
    //         }else//
    //         {
    //             return;
    //         }
    //     }else
    //     {
    //         moved = false;
    //     }
    }
}
