using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    public GameObject deathscreen;
    public Transform respawn; 
    public GameObject player; 
    public QTE qte;


    public void PlayerDied()
    {
        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		deathscreen.SetActive(true);
    }

    public void RevivePlayer()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Reposiciona antes de ativar
        player.transform.position = respawn.position;

        // Se tiver script de controle, reativa
        if (qte.disable != null)
        {
            qte.disable.EnablePlayer();
        }

        // Esconde a tela de morte
        deathscreen.SetActive(false);

        if (qte != null)
        {
            qte.ResetArrest();
        }

        // Reativa o jogador e o posiciona no ponto de respawn
        /*player.transform.position = respawnPoint.position;
        player.SetActive(true);
        deathScreenUI.SetActive(false);*/
    }
}