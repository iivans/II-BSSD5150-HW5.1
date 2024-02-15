using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyObject : MonoBehaviour
{
    public GameObject player; // Reference to the player object
    private int attachedChildrenCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // First game object involved in the collision
        GameObject instigator = collision.contacts[0].otherCollider.gameObject;

        // Check if the collision involves a CyclingObject component
        CyclingObject cyclingObjectA = collision.gameObject.GetComponent<CyclingObject>();
        CyclingObject cyclingObjectB = instigator.GetComponent<CyclingObject>();

        if (cyclingObjectA != null && cyclingObjectB != null)
        {
            int stateA = cyclingObjectA.state;
            int stateB = cyclingObjectB.state;

            // Check if the states match
            if (stateA == stateB)
            {
                // Stop the cycle of the collided objects
                cyclingObjectA.StopCycle();
                cyclingObjectB.StopCycle();

                // Set the collided object as a child of the current object
                collision.gameObject.transform.SetParent(transform);

                // Increment the attached children count
                attachedChildrenCount++;

                // Check if the player has attached 5 children
                if (attachedChildrenCount == 5)
                {
                    Debug.Log("You Win");
                    // Find the GameController and stop counting down
                    GameController gameController = FindObjectOfType<GameController>();
                    if (gameController != null)
                    {
                        gameController.StopCountingDown();
                    }
                }
            }
            else
            {
                // Penalty for touching an object of the wrong color
                DetachAllChildren();
            }
        }
    }

    private void DetachAllChildren()
    {
        // Detach all children from the player object
        foreach (Transform child in player.transform)
        {
            child.SetParent(null);
        }
    }
}
