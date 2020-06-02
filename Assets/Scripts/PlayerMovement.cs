using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed;
    public float gravity;
    public float jumpHeight;

    private Vector3 velocity;

    public Transform groundCheck;
    public float groundRadius;
    public LayerMask groundMask;
    private bool isGrounded 
    {
        get { return Physics.CheckSphere(groundCheck.position, groundRadius, groundMask); }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        BasicMovement();

        if (CanJump())
        {
            Jump();
        }

        //SetGravity();
    }
    private void BasicMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    private void SetGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y -= gravity;
        controller.Move(velocity * Mathf.Pow(Time.deltaTime, 2));
    }
    private void Jump()
    {
        velocity.y = jumpHeight * 10f * gravity;
    }
    private bool CanJump()
    {
        return Input.GetButtonDown("Jump") && isGrounded;
    }
}
