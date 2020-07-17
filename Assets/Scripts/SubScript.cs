using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScript : MonoBehaviour
{
    AudioSource audioSource;

    public GameObject lights;

    float timer, subtime;

    bool subInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        subtime = 20;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Sub()
    {
        subInProgress = true;
        audioSource.Play();
        lights.SetActive(true);
        StartCoroutine(Helper.WaitFor(14, () => { subInProgress = false; lights.SetActive(false); }));
    }

    // Update is called once per frame
    void Update()
    {
        if (subInProgress || GameController.Instance.hasGameEnded || !GameController.Instance.hasGameStarted) return;
        timer += Time.deltaTime;
        if(timer > subtime)
        {
            timer = 0;
            Sub();
        }
    }

}
