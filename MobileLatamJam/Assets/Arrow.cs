using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //keep reference of the conductor:

    public float speed = 3;
    public Rigidbody2D rb;

    private string direction;
    
    public Sprite DownArrow;
    public Sprite UpArrow;
    public Sprite LeftArrow;
    public Sprite RightArrow;

    public void Initialize(string direct)
    {
        direction = direct;

        switch(direction)
        {
            case "Right":
            
                rb.velocity = transform.right * speed;
                break;

            case "Left":

                rb.velocity = -transform.right * speed;
                break;

            case "Up":
                rb.velocity = transform.up * speed;
                break;

            case "Down":
                rb.velocity = -transform.up * speed;
                break;
        }
    }

    void OnTrigger2D(Collider2D hitInfo)
    { 
        Destroy(gameObject);
    }
}