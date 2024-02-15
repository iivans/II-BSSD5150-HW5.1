using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDragWithDynamics : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 offset;
    private Vector3 mousePosition;
    private Vector2 mouseForce;
    private Vector3 lastPosition;
    private bool selected = false;
    public float maxSpeed = 10;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (selected)
        {
            // Force based on the last position and distance the mouse has moved
            mouseForce = (mousePosition - lastPosition) / Time.deltaTime;
            // Don't let the force get larger than maxSpeed
            mouseForce = Vector2.ClampMagnitude(mouseForce, maxSpeed);
            lastPosition = mousePosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Same detection of click as before
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject)
            {
                // If the clicked GameObject is the GameObject belonging to this script
                if (targetObject.transform.gameObject == gameObject)
                {
                    rb2d.freezeRotation = true;
                    offset = gameObject.transform.position - mousePosition;
                    selected = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && selected)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.freezeRotation = true;
            selected = false;
        }
    }

    void FixedUpdate()
    {
        if (selected)
        {
            rb2d.MovePosition(mousePosition + offset);
        }
    }
}
