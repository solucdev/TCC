using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public PlayerCrouchCam crouch;
    public Camera cam;
    public float speed;
    private float isp;
    public GameObject orientation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isp = speed;
    }

    void Update()
    {
        Move();
        Run();
      
    }
    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 move = (orientation.transform.right * moveX + orientation.transform.forward * moveZ).normalized * speed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }
    private void Run()
    {

        if (crouch.isdown)
        {
            speed = 1;
        }

        else
        {
            speed = isp;
        }

    }
}
