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

        switch(horizontal, vertical)
        {
            //  Up - 1
            case (0, 1):
                if (dir == "NW" || dir == "NE")
                {
                    animator.SetInteger("direction", 2);
                }
                else
                {
                    animator.SetInteger("direction", 1);
                }
                break;
            // Down - 2
            case (0, -1):
                if (dir == "NW" || dir == "NE")
                {
                    animator.SetInteger("direction", 2);
                }
                else
                {
                    animator.SetInteger("direction", 1);
                }
                break;
            // Right - 3
            case (1, 0):
                if (dir == "NE" || dir == "SE")
                {
                    animator.SetInteger("direction", 3);
                }
                else
                {
                    animator.SetInteger("direction", 4);
                }
                break;
            // Left - 4
            case (-1, 0):
                if (dir == "NE" || dir == "SE")
                {
                    animator.SetInteger("direction", 4);
                }
                else
                {
                    animator.SetInteger("direction", 3);
                }
                break;
            // Up & Right - 5
            case (1, 1):
                if (dir == "NW" || dir == "NE")
                {
                    animator.SetInteger("direction", 5);
                }
                else
                {
                    animator.SetInteger("direction", 6);
                }
                break;
            // Up & Left - 6
            case (-1, 1):
                if (dir == "NW" || dir == "NE")
                {
                    animator.SetInteger("direction", 6);
                }
                else
                {
                    animator.SetInteger("direction", 5);
                }
                break;
            // Down & Right - 7
            case (1, -1):
                if (dir == "NW" || dir == "NE")
                {
                    animator.SetInteger("direction", 7);
                }
                else
                {
                    animator.SetInteger("direction", 8);
                }
                break;
            // Down & Left -- 8
            case (-1, -1):
                if (dir == "NW" || dir == "NE")
                {
                    animator.SetInteger("direction", 8);
                }
                else
                {
                    animator.SetInteger("direction", 7);
                }
                break;
            default:
                animator.SetInteger("direction", 0);
                break;
        }

        Debug.Log("rotation = " + transform.eulerAngles.y);

        if (transform.eulerAngles.y <= 270 && transform.eulerAngles.y > 180)
        {
            dir = "SW";
        }
        else if (transform.eulerAngles.y <= 180 && transform.eulerAngles.y > 90)
        {
            dir = "SE";
        }
        else if (transform.eulerAngles.y <= 90 && transform.eulerAngles.y > 0)
        {
            dir = "NE";
        }
        else if (transform.eulerAngles.y <= 360 && transform.eulerAngles.y > 270)
        {
            dir = "NW";
        }
        else
        {
            dir = "None";
        }

        Debug.Log(dir);
    }

    private void Rotate()
    {
        mousePosition = Input.mousePosition;
        playerPosition = cam.WorldToScreenPoint(transform.position);

        Debug.Log(mousePosition);
        Vector2 direction = mousePosition - playerPosition;
        angle = new Vector3(direction.x, 0, direction.y);
        transform.rotation = Quaternion.LookRotation(angle);
    }
}
