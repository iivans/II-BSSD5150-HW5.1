using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // First game object involved in the collision
        GameObject instigator = collision.contacts[0].otherCollider.gameObject;

        int stateA = collision.gameObject.GetComponent<CyclingObject>().state;
        int stateB = instigator.GetComponent<CyclingObject>().state;

        if (stateA == stateB)
        {
            collision.gameObject.GetComponent<CyclingObject>().StopCycle();
            collision.gameObject.transform.SetParent(transform);
        }
    }
}
