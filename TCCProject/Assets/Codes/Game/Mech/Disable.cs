using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    [SerializeField] PlayerCrouchCam orientation;
    [SerializeField] PlayerCam playercam;
    [SerializeField] PlayerMove playermove;
    [SerializeField] CamBreath cambreath;

   public void DisablePlayer()
    {
        playermove.enabled = false;
        orientation.enabled = false;
        playercam.enabled = false;
        cambreath.enabled = false;
        Rigidbody rb = playermove.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }
    public void EnablePlayer()
    {
        orientation.enabled = true;
        playercam.enabled = true;
        playermove.enabled = true;
        cambreath.enabled = true;
    }
}
