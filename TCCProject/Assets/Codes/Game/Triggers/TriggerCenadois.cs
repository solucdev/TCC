using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TriggerCenadois : MonoBehaviour
{
    public GameObject playerController;
    public Animator inimigoAnimator;
    public Disable disable;
    public GameObject texto;

    [SerializeField] Transform head;
    [SerializeField] Transform cam;
    [SerializeField] Camera mainCamera;


    private bool hasTriggered = false;
    private bool arrest = false;

    private void Update()
    {
        if (arrest)
        {
            // Travar a câmera olhando para o ponto de interesse
            cam.LookAt(head);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            StartZoom();
            hasTriggered = true;
            arrest = true;

            // Desativa os scripts de controle do jogador
            disable.DisablePlayer();

            // Inicia a animação do inimigo
            inimigoAnimator.Play("inimigocena2");

            // Espera a animação terminar
        }

    }

   

    void OnAnimationEnd()
    {
        arrest = false;

        texto.SetActive(true);
        // Reativa os scripts de controle do jogador
        disable.EnablePlayer();
    }


    public void StartZoom()
    {
        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        float targetFOV = 30f; // Zoom desejado
        float duration = 1f;
        float startFOV = mainCamera.fieldOfView;
        float time = 0;

        while (time < duration)
        {
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        mainCamera.fieldOfView = targetFOV;
    }

}
