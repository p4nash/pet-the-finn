using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpritePlayer : MonoBehaviour
{
    public Sprite[] frames;
    int currentFrame; float frameTimer;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        frameTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frameTimer += Time.deltaTime*speed;
        if(frameTimer > frames.Length)
        {
            frameTimer = 0;
        }
        currentFrame = (int) frameTimer;

        gameObject.GetComponent<Image>().sprite = frames[currentFrame];
    }
}
