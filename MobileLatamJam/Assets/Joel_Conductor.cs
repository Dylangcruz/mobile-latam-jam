using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Joel_Conductor : MonoBehaviour
{

    public GameObject BeatCollapsePrefab;
    // used to substract empty offsets to get to actual song
    public float songOffset;

    // beat locations added into this array

    public float[] track;

    // start positionX of notes

    public float startLineX;

    // the position of music notes in Y
    public float posY;

    // the position where the notes reach the end
    public float finishLineX;

    // the position where the note should be destroyed

    public float removeLineX;

    // seconds each beat last
    public float secondsPerBeat;

    //how many beats are contained on the screen

    public float BeatsShownOnScreen = 4f;

    //this plays the song

    public AudioSource songAudioSource;

    //plays the beat
    public AudioSource beatAudioSource;

    // current song position

    [NonSerialized] public float songposition;

    // next index for the array "track"

    private int indexOfNextNote;

    // queue, keep references of the musicnodes which are currently on screen.

    private Queue<BeatCollapse> notesOnScreen;

    //record the time passed 
    private float dsptimesong;

    private bool songStarted = false;

    void PlayerInputted()
    {
        // start the song if it hasn't started yet
        if(!songStarted)
        {
            songStarted = true;
            StartSong();
            return;
        }

        //play the beat soudn

        beatAudioSource.Play();

        if (notesOnScreen.Count > 0)
        {

            //get the front note
            BeatCollapse frontNote = notesOnScreen.Peek();

            //distance from the note to the finish line
            float offset = Mathf.Abs(frontNote.gameObject.transform.position.x -finishLineX);





        }


    }

    void Start()
    {
        //initialize stuff
        notesOnScreen = new Queue<BeatCollapse>();

        indexOfNextNote = 0;

    }

    void StartSong()
    {
        dsptimesong = (float) AudioSettings.dspTime;
        songAudioSource.Play();
    }

    void Update()
    {

        // cualculate song position
        songposition = (float) (AudioSettings.dspTime - dsptimesong - songOffset);

        float beatToShow = songposition/secondsPerBeat + BeatsShownOnScreen;


        if (indexOfNextNote < track.Length && track[indexOfNextNote] < beatToShow)
        {
            BeatCollapse beatFollower = ((GameObject) Instantiate(BeatCollapsePrefab, Vector2.zero, Quaternion.identity)).GetComponent<BeatCollapse>();

            beatFollower.Initialize(this, startLineX,finishLineX, removeLineX, posY, track[indexOfNextNote]);

            notesOnScreen.Enqueue(beatFollower);

            indexOfNextNote++;
        }

        //loop to queue th check if any of them reaches the finish line.

        if (notesOnScreen.Count > 0)
        {
            BeatCollapse currNote = notesOnScreen.Peek();

            if(currNote.transform.position.x >= finishLineX)
            {
                notesOnScreen.Dequeue();


            }
        }


    }










}
