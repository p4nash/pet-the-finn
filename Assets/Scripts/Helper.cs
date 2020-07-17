using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public delegate void FunctionExecute();
    public static IEnumerator WaitFor(float timer, FunctionExecute func)
    {
        while (timer > 0)
        {
            timer-=0.2f;
            yield return new WaitForSeconds(0.2f);
        }
        func();
    }

    public static IEnumerator WaitForFrame(int frames, FunctionExecute func)
    {
        while(frames > 0)
        {
            frames--;
            yield return new WaitForEndOfFrame();
        }
        func();
    }
}
