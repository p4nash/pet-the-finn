using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScript : MonoBehaviour
{
    AudioSource audioSource;

    public GameObject lights;

    delegate void FunctionExecute();

    float timer, subtime;

    bool subInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        subtime = 10;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Sub()
    {
        subInProgress = true;
        audioSource.Play();
        lights.SetActive(true);
        StartCoroutine(WaitFor(14, () => { subInProgress = false; lights.SetActive(false); }));
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

    IEnumerator WaitFor(float timer, FunctionExecute func)
    {
        while (timer > 0)
        {
            timer--;
            yield return new WaitForSeconds(1);
        }
        func();
    }
}
