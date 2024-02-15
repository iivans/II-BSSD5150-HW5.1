using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform timer;

    [SerializeField]
    private Transform timerTarget;

    private CyclingObject[] circles;
    private int gameTime = 10;
    private int countDown;
    private float distance;
    private GameObject player;

    private bool isCountingDown = true; 

    private void OnEnable()
    {
        countDown = gameTime;

        // Calculate and save the distance to move offscreen
        distance = timer.position.x - timerTarget.position.x;

        circles = FindObjectsOfType<CyclingObject>();

        foreach (CyclingObject cyc in circles)
        {
            if (cyc.gameObject.CompareTag("Player"))
            {
                cyc.waitTime = Random.Range(0.5f, 1.0f);
            }
            else
            {
                // Save reference to player
                player = cyc.gameObject;
            }
        }
    }

    private void Start()
    {
        StartCoroutine("CountDown");
    }

    private IEnumerator CountDown()
    {
        while (isCountingDown)
        {
            yield return new WaitForSecondsRealtime(1.0f);
            countDown--;

            // Move a percentage of the distance to be offscreen
            timer.position = new Vector3(distance / gameTime, 0, 0);

            if (countDown == 0)
            {
                Debug.Log("You Lose");
                player.transform.DetachChildren();
            }
        }
    }

    // Public function to stop counting down
    public void StopCountingDown()
    {
        isCountingDown = false;
    }
}