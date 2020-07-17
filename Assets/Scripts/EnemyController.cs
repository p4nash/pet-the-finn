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

    public AudioClip manOof, footsteps;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        leftMax = transform.position - new Vector3(maxMovement, 0, 0);
        rightMax = transform.position + new Vector3(maxMovement, 0, 0);
        audioSource = gameObject.GetComponent<AudioSource>();
        turnAround = false;
    }

    public void DestroyEnemy()
    {
        if (!Active) return;
        Active = false;
        audioSource.PlayOneShot(manOof);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active || GameController.Instance.hasGameEnded) return;

        if(type == "left")
        {
            audioSource.panStereo = -1;
            audioSource.PlayOneShot(footsteps);

            if (transform.position.x < rightMax.x && !turnAround)
            {
                transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
            }
            else if(transform.position.x >= rightMax.x && !turnAround)
            {
                turnAround = true;
            }
            else if (turnAround && transform.position.x > originalPosition.x)
            {
                transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
            }
            else if (turnAround && transform.position.x <= originalPosition.x)
            {
                GameController.Instance.DoDamage();
                Destroy(this.gameObject);
            }
        }
        else if (type == "right")
        {
            audioSource.panStereo = 1;
            audioSource.PlayOneShot(footsteps);

            if (transform.position.x > leftMax.x && !turnAround)
            {
                transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
            }
            else if (transform.position.x <= leftMax.x && !turnAround)
            {
                turnAround = true;
            }
            else if (turnAround && transform.position.x < originalPosition.x)
            {
                transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
            }
            else if(turnAround && transform.position.x >= originalPosition.x)
            {
                GameController.Instance.DoDamage();
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
