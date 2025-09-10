using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpforce;
    [SerializeField] bool grounded;
    [SerializeField] Rigidbody rb;
    [SerializeField] QuadVentory invset;
    [SerializeField] List<Transform> foots = new List<Transform>();

    private void FixedUpdate()
    { CheckGround(); }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded && !invset.open)
        { Jump(); }
    }
    private void CheckGround()
    {
        for (int i = 0; i < foots.Count; i++)
        {
            Transform ax = foots[i].transform;
            grounded = Physics.Raycast(ax.position, Vector3.down, 0.15f);
            Debug.DrawRay(ax.position, Vector3.down * 0.15f, Color.red);
            if (grounded)
            {
                break;
            }
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpforce * 1000, ForceMode.Impulse);
    }
}
