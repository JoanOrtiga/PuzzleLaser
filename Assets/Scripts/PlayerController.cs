using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MouseLook))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Vector3 directionX, directionZ;
    private MouseLook look;
    private Rigidbody rb;
    public float speed = 2.5f;
    public float runSpeed = 7.5f;
    [Range(0f, 5f)]public float jumpForce = 5f;

    private float Speed
    {
        get
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                return runSpeed;
            }
            return speed;
        }
    }
    void Start()
    {
        look = GetComponent<MouseLook>();
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Movement();
    }
    void Update()
    {
        if (isGroudned() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    void Movement()
    {
        //direction with camera
        //X
        directionX = look.playerCamera.right;
        directionX.y = 0;
        directionX.Normalize();
        //Z
        directionZ = look.playerCamera.forward;
        directionZ.y = 0;
        directionZ.Normalize();

        //Change character velocity to its direction
        rb.velocity = directionZ * Input.GetAxis("Vertical") * Speed + directionX * Input.GetAxis("Horizontal") * Speed + Vector3.up * rb.velocity.y;
    }
    private bool isGroudned()
    {
        RaycastHit groundHit;
        return Physics.Raycast(transform.position, -transform.up, out groundHit, 1.25f);
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce * 100, ForceMode.Impulse);
    }
}
