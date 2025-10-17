using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TriggerCenadois : MonoBehaviour
{
    public GameObject playerController;
    public Animator inimigoAnimator;


    private void OnTriggerEnter(Collider other)
    {
        inimigoAnimator.Play("inimigocena2");
        playerController.SetActive(false);
    }

    public void OnAnimationEnd()
    {
        playerController.SetActive(true);
    }
}
