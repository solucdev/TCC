using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject canvasVideo;
    public float maxDistance = 10f;
    public Camera playerCamera;
    public GameObject texto;

    private bool isPlayerNear = false;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        canvasVideo.SetActive(false);
        Time.timeScale = 1f;
    }


    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            /*canvasVideo.SetActive(true);
            videoPlayer.Play();
            texto.SetActive(false);
            Time.timeScale = 0f;*/
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {

                Debug.Log("Raycast atingiu: " + hit.transform.name); // Verifica o que foi atingido

                if (hit.transform == transform)
                {
                    canvasVideo.SetActive(true);
                    videoPlayer.Play();
                    texto.SetActive(false);
                    Time.timeScale = 0f;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }

        texto.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
        texto.SetActive(false);
    }
}
