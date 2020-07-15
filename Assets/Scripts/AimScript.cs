using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    public bool collLeft, collRight, collUp, collDown;

    // Start is called before the first frame update
    void Start()
    {
        collLeft = collRight = collUp = collDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Collision left with " + collision.gameObject.name);
        if (collision.gameObject.name == "border left")
        {
            collLeft = false;
        }
        else if (collision.gameObject.name == "border right")
        {
            collRight = false;
        }
        else if (collision.gameObject.name == "border top")
        {
            collUp = false;
        }
        else if (collision.gameObject.name == "border bottom")
        {
            collDown = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        if(collision.gameObject.name == "border left")
        {
            collLeft = true;
        }
        else if (collision.gameObject.name == "border right")
        {
            collRight = true;
        }
        else if (collision.gameObject.name == "border top")
        {
            collUp = true;
        }
        else if (collision.gameObject.name == "border bottom")
        {
            collDown = true;
        }
    }
}
