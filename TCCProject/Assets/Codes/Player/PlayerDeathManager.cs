using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeathManager : MonoBehaviour
{
    public GameObject deathScreenUI; // Painel com botão "Reviver"
    public Transform respawnPoint;   // Ponto específico do cenário
    public GameObject player;        // Referência ao jogador
    private Disable disable => FindObjectOfType<Disable>();
    //public Disable disable;
    public QTE qteScript; // Referência ao script QTE


    public void PlayerDied()
    {
        Cursor.lockState = CursorLockMode.None;
        deathScreenUI.SetActive(true);
    }

    public void RevivePlayer()
    {

        // Reposiciona antes de ativar
        player.transform.position = respawnPoint.position;

        // Se tiver script de controle, reativa
        if (disable != null)
        {
            disable.EnablePlayer();
        }

        // Esconde a tela de morte
        deathScreenUI.SetActive(false);

        if (qteScript != null)
        {
            qteScript.ResetArrest();
        }

        // Reativa o jogador e o posiciona no ponto de respawn
        /*player.transform.position = respawnPoint.position;
        player.SetActive(true);
        deathScreenUI.SetActive(false);*/
    }
}