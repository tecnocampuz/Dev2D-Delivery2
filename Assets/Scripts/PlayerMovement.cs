using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public bool IsMoving => isMoving;

    [SerializeField]
    private float Speed = 5.0f;

    private bool isMoving;
    PlayerInput input;
    Rigidbody2D rb;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = new Vector2(input.movementHorizontal, input.movementVertical) 
                            * (input.sneak ? Speed/2 : Speed);
        rb.velocity = direction;
        isMoving = direction.magnitude > 0.01f;

        if (isMoving)
        {
            //LookAt((Vector2)transform.position + direction);
            // look at the direction of movement
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    void LookAt(Vector2 targetPosition)
    {
        float angle = 0.0f;
        Vector3 relative = transform.InverseTransformPoint(targetPosition);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }
}
