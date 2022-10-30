using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    private float secPerBeat;

    //Current song position, in seconds
    private float songPosition;

    //Current song position, in beats
    public int songPositionInBeats;

    //How many seconds have passed since the song started
    private float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    private AudioSource musicSource;


    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;


    //to let other code know when the beat happens
    public bool onBeat;
    public bool preciseBeat;

    public float bufferTimeInBeats = .49999f;
    // Start is called before the first frame update


    void Start()
    {
        onBeat = false;


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
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats =  (int)Mathf.Floor(songPosition / secPerBeat);
        if (songPosition >0 && songPosition<10 )
        {
            Debug.Log("seconds away from beat" + SecondsAwayFromBeat());
        }

    }

    public float SecondsAwayFromBeat()
    {
        float timeOffBeat = Mathf.Abs((songPositionInBeats * secPerBeat) - songPosition); 
        return timeOffBeat;
    }
}
