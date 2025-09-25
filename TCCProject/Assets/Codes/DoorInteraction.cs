using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public AudioSource doorAudio;         // Arraste aqui o AudioSource com o som da porta
    public string nextSceneName;          // Nome da próxima cena
    private bool playerNear = false;      // Se o jogador está perto da porta
    private bool doorUsed = false;        // Se já tocou o som
    private bool loadingScene = false;    // Para não carregar várias vezes
    public AudioSource doorOpeningAudio;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!doorUsed)
            {
                // Primeira vez apertando E → toca o som
                doorAudio.Play();
                doorUsed = true;
                Debug.Log("um");
            }
            else if (!loadingScene)
            {
                // Segunda vez → troca de cena quando o som terminar
                doorOpeningAudio.Play();
                StartCoroutine(LoadSceneAfterSound());
                loadingScene = true;
                Debug.Log("dois");
            }
        }
    }

    private System.Collections.IEnumerator LoadSceneAfterSound()
    {
        // Espera terminar o som
        while (doorAudio.isPlaying)
        {
            yield return null;
        }

        // Carrega a próxima cena
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("oi");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}