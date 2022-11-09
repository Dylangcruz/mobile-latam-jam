using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Beat_Indicator : MonoBehaviour
{

    //keep reference of the conductor:

    public Conductor conductor;

    public float startX;
    public float endX;
    public float beat;
    public Vector3 startVector;
    public Vector3 endVector;
    private float interpolationRatio;

    public void Initialize(Conductor conductor, float startX, float endX, float posY, float beat)
    {
        this.conductor = conductor;
        this.startX = startX;
        this.endX = endX;
        this.beat = beat;

        this.startVector= new Vector2(startX, posY);
        this.endVector= new Vector2(endX, posY);
    }

    void Update()
    {
        interpolationRatio = (conductor.songPositionInBeatsUnfloored-beat)/2; // (current beat position - beat it was spawned)/how many notes i want on screen    

        //Update position of the note according to the position of the song
        transform.localPosition = Vector3.Lerp(startVector, endVector,interpolationRatio);

        //Remove itself when out of the screen

        if(interpolationRatio>1)
        {
           Destroy(gameObject);
        }
    }
}
