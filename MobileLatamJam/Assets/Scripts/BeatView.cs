using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatView : MonoBehaviour
{
    [SerializeField] GameObject ConductorObject;
    public Conductor conductorinstance;

    Vector3 Size;
    // Start is called before the first frame update
    void Start()
    {
        conductorinstance = GameObject.Find("Conductor").GetComponent<Conductor>();
    }



    // Update is called once per frame
    void Update()
    {
        if(conductorinstance.canSwipe)
        {
            Size = Vector3.one * 3;
        }
        else
        {
            Size = Vector3.one;
        }
        transform.localScale = Size;
    }
}
