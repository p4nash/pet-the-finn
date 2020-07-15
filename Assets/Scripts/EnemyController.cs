using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Vector3 leftMax, rightMax;
    public Vector3 originalPosition;
    public float maxMovement;

    public string type; //0 means it spawned left, 1 means it spawned right

    public float speed;

    bool turnAround;

    public bool Active;

    // Start is called before the first frame update
    void Start()
    {
        leftMax = transform.position - new Vector3(maxMovement, 0, 0);
        rightMax = transform.position + new Vector3(maxMovement, 0, 0);
        turnAround = false;
    }

    private void Awake()
    {
        //originalPosition = this.transform.position;
        //Debug.Log("Original position is " + originalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active) return;

        if(type == "left")
        {
            if(transform.position.x < rightMax.x && !turnAround)
            {
                transform.position += new Vector3(1, 0, 0) * speed;
            }
            else if(transform.position.x >= rightMax.x && !turnAround)
            {
                turnAround = true;
            }
            else if (turnAround && transform.position.x > originalPosition.x)
            {
                transform.position += new Vector3(-1, 0, 0) * speed;
            }
            else if (turnAround && transform.position.x <= originalPosition.x)
            {
                Destroy(this.gameObject);
            }
        }
        else if (type == "right")
        {
            if (transform.position.x > leftMax.x && !turnAround)
            {
                transform.position += new Vector3(-1, 0, 0) * speed;
            }
            else if (transform.position.x <= leftMax.x && !turnAround)
            {
                turnAround = true;
            }
            else if (turnAround && transform.position.x < originalPosition.x)
            {
                transform.position += new Vector3(1, 0, 0) * speed;
            }
            else if(turnAround && transform.position.x >= originalPosition.x)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void Activate()
    {
        Debug.Log("Enemy activated");
        Active = true;
    }
}
