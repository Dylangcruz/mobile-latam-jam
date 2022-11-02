using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCollapse : MonoBehaviour
{

    //keep reference of the conductor:

    public Joel_Conductor conductor;

    public float startX;
    public float endX;
    public float removeLineX;
    public float beat;

    private SpriteRenderer spriteRenderer;

    public void Initialize(Joel_Conductor conductor, float startX, float endX, float removeLineX, float posY, float beat)
    {
        this.conductor = conductor;
        this.startX = startX;
        this.endX = endX;
        this.beat = beat;
        this.removeLineX = removeLineX;

        //set to initial position
        transform.position = new Vector2(startX, posY);

        spriteRenderer = GameObject.Find("Test").GetComponent<SpriteRenderer>();

    }

    void Update()
    {

        //Update position of the note according to the position of the song

        transform.position = new Vector2(startX + (endX -startX) * (1f - (beat - conductor.songposition/conductor.secondsPerBeat) / 4f ), transform.position.y);

        //Remove itself when out of the screen

        if(transform.position.x > removeLineX)
        {
            Destroy(gameObject);
        }
    }

    /*
    // Start is called before the first frame update
    void Start()
    {

        secPerBeat = 60f / bpm;

        dpsTimeSong = (float) AudioSettings.dpsTime;

        GetComponent<AudioSource>().Play();
        
    }

    // Update is called once per frame
    void Update()
    {

        songPosition = (float)(AudioSettings.dspTime - dpsTimeSong )

        songPosInBeats = songPosition / secPerBeat;
        
    }

    */
}

