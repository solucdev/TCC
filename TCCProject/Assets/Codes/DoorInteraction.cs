using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public AudioSource doorAudio;       
    public string nextSceneName;     
    private bool playerNear = false;   
    private bool doorUsed = false;       
    private bool loadingScene = false;    
    public AudioSource doorOpeningAudio;
    public GameObject texto1;
    public GameObject texto2;
    public GameObject ui;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!doorUsed)
            {
                doorAudio.Play();
                texto1.SetActive(false);
                texto2.SetActive(true);
                doorUsed = true;
                Debug.Log("um");
            }
            else if (!loadingScene)
            {
               
                doorOpeningAudio.Play();
                StartCoroutine(LoadSceneAfterSound());
                loadingScene = true;
                Debug.Log("dois");
            }
        }
    }

    private System.Collections.IEnumerator LoadSceneAfterSound()
    {
        while (doorAudio.isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            ui.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            ui.SetActive(false);
        }
    }
}