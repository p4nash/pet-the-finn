using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    bool hasGameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasGameStarted)
        {
            if (Input.anyKeyDown)
            {
                hasGameStarted = true;
                SceneManager.LoadScene("Game");
            }
            else
                return;
        }
    }
}
