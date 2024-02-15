using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclingObject : MonoBehaviour
{
    public float waitTime = 0.5f;
    public int state = 0;
    SpriteRenderer sr;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.cyan;
        coroutine = CycleColor(waitTime);
        StartCoroutine(coroutine);
    }

    public void StopCycle()
    {
        StopAllCoroutines();
    }

    private IEnumerator CycleColor(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        StopCoroutine(coroutine);
        coroutine = CycleColor(waitTime);
        StartCoroutine(coroutine);

        state++;

        if (state == 0)
        {
            sr.color = Color.cyan;
        }
        else if (state == 1)
        {
            sr.color = Color.green;
        }
        else if (state == 2)
        {
            sr.color = Color.red;
        }
        else
        {
            state = 0;
            sr.color = Color.cyan;
        }
    }
}
