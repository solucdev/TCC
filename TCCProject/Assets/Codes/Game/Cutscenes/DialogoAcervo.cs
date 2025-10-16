using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoAcervo : MonoBehaviour
{
    public GameObject dialogueManager; // Referência ao sistema de diálogo
    public GameObject playerController; // Script ou objeto que controla o jogador
    public GameObject keyObject; // A chave que será ativada

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            playerController.SetActive(false); // Desativa controle do jogador
            dialogueManager.SetActive(true); // Inicia diálogo
        }
    }

    public void EndCutscene()
    {
        dialogueManager.SetActive(false);
        playerController.SetActive(true); // Reativa controle do jogador
        keyObject.SetActive(true); // Ativa a chave
    }
}
