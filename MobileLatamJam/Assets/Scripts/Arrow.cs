using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //keep reference of the conductor:

    public float speed = 10;
    public Rigidbody2D rb;

    private string direction;
    private Vector3 initialPosition;
    private int range=3;

    public void Initialize(string direct, int expectedRange)
    {
        direction = direct;
        range = expectedRange;
        initialPosition =transform.position;

        switch(direction)
        {
            case "Right":
                break;

            case "Left":

                //transform.rotation = Quaternion.Euler(0,180,0);
                break;

            case "Up":
                //transform.rotation = Quaternion.Euler(0,90,0);
                break;

            case "Down":
                //transform.rotation = Quaternion.Euler(0,270,0);
                break;
        }
        rb.velocity = transform.right * speed;
    }
    void Update()
    {
        if(Mathf.Abs(transform.position.x - initialPosition.x) > range  
        || Mathf.Abs(transform.position.y - initialPosition.y) > range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    { 
        hitInfo.GetComponent<PlayerHealth>().Damage(1);
        Debug.Log("Arrow hit: " + hitInfo.name);
        Destroy(gameObject);
    }
}