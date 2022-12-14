using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
  
public class Conductor : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public int songPositionInBeats;
    public float songPositionInBeatsUnfloored;
    public int lastBeat; //so i can tell when the beat changes

    //How many seconds have passed since the song started
    private float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    private AudioSource musicSource;


    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

   //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;
    //the number of beats in each loop
    public float beatsPerLoop;

    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;

    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;

    //Conductor instance
    public static Conductor instance;

    public GameObject beatIndicatorPrefab;



    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {       

        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeatsUnfloored = songPosition / secPerBeat;
        songPositionInBeats =  (int)Mathf.Floor(songPositionInBeatsUnfloored);

        if (songPositionInBeatsUnfloored >= (completedLoops + 1) * beatsPerLoop)
        {
        completedLoops++;
        }

        loopPositionInBeats = songPositionInBeatsUnfloored - completedLoops * beatsPerLoop;

        if(songPositionInBeats != lastBeat)
        {
           // var graphToScan = AstarPath.active.data.gridGraph;
           // AstarPath.active.Scan(graphToScan);

            lastBeat = songPositionInBeats;
            //instantiate indicators
            var beatIndicatorRight = Instantiate(beatIndicatorPrefab, new Vector2(250, 390), Quaternion.identity,GameObject.FindGameObjectWithTag("Canvas").transform);
            var beatIndicatorLeft = Instantiate(beatIndicatorPrefab, new Vector2(-250, 390), Quaternion.Euler(0,180,0),GameObject.FindGameObjectWithTag("Canvas").transform);

            //initialize their values
            
            beatIndicatorRight.GetComponent<Beat_Indicator>().Initialize(this, 250, 30, 390, songPositionInBeats);
            beatIndicatorLeft.GetComponent<Beat_Indicator>().Initialize(this, -250, -30, 390, songPositionInBeats);
        }
    }

    public float SecondsAwayFromBeat()
    {
        float timeOffBeat = Mathf.Abs((songPositionInBeats * secPerBeat) - songPosition); 
        return timeOffBeat;
    }
}
