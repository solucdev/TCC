using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    public GameObject screenPause;
    public GameObject deathscreen;
    public Transform respawn; 
    public GameObject player; 
    public QTE qte;


    public void PlayerDied()
    {
        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
        screenPause.SetActive(false);
		deathscreen.SetActive(true);
        Time.timeScale = 1;
    }

    public void RevivePlayer()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.transform.position = respawn.position;

        if (qte.disable != null)
        {
            qte.disable.EnablePlayer();
        }

        deathscreen.SetActive(false);

        if (qte != null)
        {
            qte.ResetArrest();
        }

    }
}