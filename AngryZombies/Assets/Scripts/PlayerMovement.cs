using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 angle;
    [Header("Objects/Components")]
    public Rigidbody rb;
    public CharacterController controller;
    public Camera cam;
    public Animator animator;

    private Vector3 mousePosition;
    private Vector3 playerPosition;

    [Header("Variables")]
    [SerializeField]
    private float speed = 5f;
    private float horizontal = 0f;
    private float vertical = 0f;

    void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        controller.Move(new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime));

        switch(horizontal, vertical)
        {
          // Up & Right
            case (1, 1):
                // animator.SetFloat("run", num);
                break;
            // Down & Right
            case (1, -1):
                break;
            // Up & Left
            case (-1, 1):
                break;
            // Down & Left
            case (-1, -1):
                break;
            // Right
            case (1, 0):
                break;
            // Left
            case (-1, 0):
                break;
            // Down
            case (0, -1):
                break;
            //  Up
            case (0, 1):
                break;
        }
    }

    private void Rotate()
    {
        mousePosition = Input.mousePosition;
        playerPosition = cam.WorldToScreenPoint(transform.position);

        Debug.Log(mousePosition);
        Vector2 direction = mousePosition - playerPosition;
        angle = new Vector3(direction.x, 0, direction.y);
        transform.rotation = Quaternion.LookRotation(-angle);
    }
}
