using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject playerController;

    public CinemachineVirtualCamera scooterCamera;
    public CinemachineVirtualCamera firstPersonCamera;

    public GameObject texto1;



    void Start()
    {
        director.stopped += OnTimelineFinished;

        // Desativa controle do jogador e define prioridade inicial
        playerController.SetActive(false);
        scooterCamera.Priority = 20;
        firstPersonCamera.Priority = 10;

        director.Play();
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        // Ativa controle do jogador
        playerController.SetActive(true);

        // Troca de prioridade para mudar a câmera
        scooterCamera.Priority = 10;
        firstPersonCamera.Priority = 20;
        texto1.SetActive(true); 
    }
}
