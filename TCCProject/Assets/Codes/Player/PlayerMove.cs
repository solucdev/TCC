using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public PlayerCrouchCam crouch;
    public Camera cam;
    public float speed;
    public float spdrun;
    private float isp;
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
        Vector3 move = (transform.right * moveX + transform.forward * moveZ).normalized * speed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }
    private void Run() {

        if (Input.GetKey(KeyCode.LeftShift) && crouch.isdown == false) 
       { speed = spdrun; 
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 90, Time.deltaTime * 10f);
		}
        else { speed = isp; 
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 75, Time.deltaTime * 10f);
		}

		if (crouch.isdown) {
            speed -= 1;
        }
    }
}
