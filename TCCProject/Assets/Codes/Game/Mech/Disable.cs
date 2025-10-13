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
        orientation.enabled = false;
        playercam.enabled = false;
        playermove.enabled = false;
        cambreath.enabled = false;
    }
    public void EnablePlayer()
    {
        orientation.enabled = true;
        playercam.enabled = true;
        playermove.enabled = true;
        cambreath.enabled = true;
    }
}
