using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 angle;
    [Header("Objects/Components")]
    public Rigidbody rb;
    public Camera cam;



    private Vector3 mousePosition;
    private Vector3 playerPosition;

    [Header("Variables")]
    [SerializeField]
    private float speed = 5f;
    private float horizontal = 0f;
    private float vertical = 0f;

    void Update()
    {
        mousePosition = Input.mousePosition;
        playerPosition = cam.WorldToScreenPoint(transform.position);

        Debug.Log(mousePosition);
        Vector2 direction = mousePosition - playerPosition;
        angle = new Vector3(direction.x, 0, direction.y);
        transform.rotation = Quaternion.LookRotation(-angle);

        Movement();
    }

    private void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rb.AddForce(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
    }
}
