using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    public bool collLeft, collRight, collUp, collDown;

    public GameObject aimCollider;
    public float sens;

    public float leftmax, rightmax, upmax, downmax;

    public Camera camera;

    public bool Active;
    public GameObject graphic;

    // Start is called before the first frame update
    void Start()
    {
        Active = false;
        collLeft = collRight = collUp = collDown = false;
    }

    public void Activate()
    {
        Active = true;
        graphic.SetActive(true);
    }

    public void Deactivate()
    {
        Active = false;
        graphic.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MoveAim();
    }

    void MoveAim()
    {
        if (Input.GetAxis("Mouse X") < 0)
        {
            //Code for action on mouse moving left
            aimCollider.transform.position += new Vector3(Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * sens;

            if (aimCollider.transform.position.x <leftmax)
            {
                aimCollider.transform.position = new Vector3(leftmax, aimCollider.transform.position.y, aimCollider.transform.position.z);
            }
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            //Code for action on mouse moving right
            aimCollider.transform.position += new Vector3(Input.GetAxis("Mouse X"), 0, 0) * Time.deltaTime * sens;
           
            if (aimCollider.transform.position.x > rightmax)
            {
                aimCollider.transform.position = new Vector3(rightmax, aimCollider.transform.position.y, aimCollider.transform.position.z);
            }
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            //Code for action on mouse moving left
            aimCollider.transform.position += new Vector3(0, Input.GetAxis("Mouse Y"), 0) * Time.deltaTime * sens;
           
            if (aimCollider.transform.position.y < downmax)
            {
                aimCollider.transform.position = new Vector3(aimCollider.transform.position.x, downmax, aimCollider.transform.position.z);
            }
        }
        if (Input.GetAxis("Mouse Y") > 0)
        {
            //Code for action on mouse moving right
            aimCollider.transform.position += new Vector3(0, Input.GetAxis("Mouse Y"), 0) * Time.deltaTime * sens;
           
            if (aimCollider.transform.position.y > upmax)
            {
                aimCollider.transform.position = new Vector3(aimCollider.transform.position.x, upmax, aimCollider.transform.position.z);
            }
        }

        if(Active)
            camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
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
