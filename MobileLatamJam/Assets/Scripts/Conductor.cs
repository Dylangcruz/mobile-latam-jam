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
    private float songPositionInBeats;

    //How many seconds have passed since the song started
    private float dspSongTime;

    //an AudioSource attached to this GameObject that will play the music.
    private AudioSource musicSource;


    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;


    //to let player swipe on certain frames
    public bool canSwipe;

    public float bufferTimeInBeats;
    // Start is called before the first frame update


    void Start()
    {
        canSwipe = false;

        bufferTimeInBeats = .35f;


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
        songPositionInBeats = songPosition / secPerBeat;
        //Debug.Log("position in beats:" +songPositionInBeats);

        if (Mathf.Abs(songPositionInBeats- Mathf.Floor(songPositionInBeats)) <= bufferTimeInBeats || Mathf.Abs(songPositionInBeats - Mathf.Ceil(songPositionInBeats)) <= bufferTimeInBeats)
            {
            canSwipe = true;
            }
        else
        {
            canSwipe = false;
        }
    }
}
