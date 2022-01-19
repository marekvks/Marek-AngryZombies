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

    string dir;

    void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = horizontal * transform.right + vertical * transform.forward;

        controller.Move(new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime));

        switch (horizontal, vertical)
        {
            //  Up - 1
            case (0, 1):
                Animate(1, 2, 3, 4);  // NSWE
                break;
            // Down - 2
            case (0, -1):
                Animate(2, 1, 4, 3);
                break;
            // Right - 3
            case (1, 0):
                Animate(3, 4, 2, 1);
                break;
            // Left - 4
            case (-1, 0):
                Animate(4, 3, 1, 2);
                break;
            // Up & Right - 5
            case (1, 1):
                Animate(5, 8, 7, 6);
                break;
            // Up & Left - 6
            case (-1, 1):
                Animate(6, 7, 5, 8);
                break;
            // Down & Right - 7
            case (1, -1):
                Animate(7, 6, 8, 5);
                break;
            // Down & Left -- 8
            case (-1, -1):
                Animate(8, 5, 6, 7);
                break;
            default:
                animator.SetInteger("direction", 0);
                break;
        }

        if ((transform.eulerAngles.y <= 45 && transform.eulerAngles.y >= 0) || (transform.eulerAngles.y >= 315 && transform.eulerAngles.y <= 360))
        {
            dir = "N";
        }
        else if (transform.eulerAngles.y <= 225 && transform.eulerAngles.y >= 135)
        {
            dir = "S";
        }
        else if (transform.eulerAngles.y < 315 && transform.eulerAngles.y > 225)
        {
            dir = "W";
        }
        else if (transform.eulerAngles.y < 135 && transform.eulerAngles.y > 45)
        {
            dir = "E";
        }
        else
        {
            dir = "None";
        }

    }

    private void Rotate()
    {
        mousePosition = Input.mousePosition;
        playerPosition = cam.WorldToScreenPoint(transform.position);

        Vector2 direction = mousePosition - playerPosition;
        angle = new Vector3(direction.x, 0, direction.y);
        transform.rotation = Quaternion.LookRotation(angle);
    }

    private void Animate(int north, int south, int west, int east)
    {
        switch(dir)
        {
            case "N":
                animator.SetInteger("direction", north);
                break;
            case "S":
                animator.SetInteger("direction", south);
                break;
            case "W":
                animator.SetInteger("direction", west);
                break;
            case "E":
                animator.SetInteger("direction", east);
                break;
        }
    }

}
