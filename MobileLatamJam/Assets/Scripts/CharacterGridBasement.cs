using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGridBasement : MonoBehaviour
{

    private bool ReadyToMove;



    // Update is called once per frame
    void Update()
    {
        Vector2 moveinput = new Vector2(Input.GetAxisRaw("horizontal"), Input.GetAxisRaw("Vertical"));
        moveinput.Normalize();

        if(moveinput.sqrMagnitude > 0.5)
        {
            if(ReadyToMove)
            {
                ReadyToMove = false;
                Move(moveinput);
            }
        }

        else
        {
            ReadyToMove = true;
        }
        
    }


    public bool Move(Vector2 direction)
    {
        if(Mathf.Abs(direction.x)< 0.5)
        {
            direction.x = 0;

        }

        else
        {
            direction.y = 0;

        }

        direction.Normalize();

        transform.Translate(direction);
        return true;
    }

    
}
