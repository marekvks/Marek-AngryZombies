using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Objects/Components")]
    public CharacterController controller;
    public Camera cam;
    public Animator animator;

    private Vector3 mousePosition;
    private Vector3 playerPosition;
    private Vector3 angle;

    public AudioSource song;
    public AudioClip pc;

    [Header("Variables")]
    [SerializeField]
    private float speed = 5f;
    private float horizontal = 0f;
    private float vertical = 0f;

    public bool canMove = true;

    private bool paused = false;

    string dir;

    void Update()
    {
        if (canMove)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;   // normalized because i dont want to move any faster when im pressing 2 keys

        if (moveDirection != Vector3.zero)
        {
            controller.SimpleMove(moveDirection * speed * Time.fixedDeltaTime);
        }

        //  Up - 1
        if (horizontal == 0 && vertical == 1)
        {
            Animate(1, 2, 3, 4);  // NSWE
        }
        // Down - 2
        else if (horizontal == 0 && vertical == -1)
        {
            Animate(2, 1, 4, 3);
        }
        // Right - 3
        else if (horizontal == 1 && vertical == 0)
        {
            Animate(3, 4, 2, 1);
        }
        // Left - 4
        else if (horizontal == -1 && vertical == 0)
        {
            Animate(4, 3, 1, 2);
        }
        // Up & Right - 5
        else if (horizontal == 1 && vertical == 1)
        {
            Animate(5, 8, 7, 6);
        }
        // Up & Left - 6
        else if (horizontal == -1 && vertical == 1)
        {
            Animate(6, 7, 5, 8);
        }
        // Down & Right - 7
        else if (horizontal == 1 && vertical == -1)
        {
            Animate(7, 6, 8, 5);
        }
        // Down & Left -- 8
        else if (horizontal == -1 && vertical == -1)
        {
            Animate(8, 5, 6, 7);
        }
        else
        {
            animator.SetInteger("direction", 0);
        }



        // imaginary compass
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
        switch (dir)
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
