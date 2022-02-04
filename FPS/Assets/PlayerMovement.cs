using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float movementSpeed = 5f;
    float gravity = 10f;

    CharacterController controller;
    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    private float jumpHeight = 2f;
    bool isGrounded;
    Vector3 velocity = new Vector3(0f, 0f, 0f);

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y += 8f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 10f;
        } else
        {
            movementSpeed = 5f;
        }

        velocity.y -= gravity * Time.deltaTime;

        Vector3 direction = transform.forward * vertical + transform.right * horizontal;
        controller.Move(direction * movementSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);     // 1/2*g*t2

        Debug.Log(isGrounded);
    }
}
