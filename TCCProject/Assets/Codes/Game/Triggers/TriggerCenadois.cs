using System.Collections;
using UnityEngine;

public class TriggerCenadois : MonoBehaviour
{
    [Header("Referências")]
    public GameObject playerController;
    public Animator inimigoAnimator;
    public Disable disable;
    public GameObject texto;

    [SerializeField] Transform head;
    [SerializeField] Transform cam;
    [SerializeField] Camera mainCamera;

    [Header("Configurações de Zoom")]
    public float zoomFOV = 30f;
    public float zoomDuration = 1f;

    private bool hasTriggered = false;
    private bool arrest = false;
    private bool zoomRestored = false;
    private float originalFOV;

    private void Start()
    {
        // Salva o FOV original da câmera
        originalFOV = Mathf.Clamp(mainCamera.fieldOfView, 30f, 90f);
    }

    private void Update()
    {
        if (arrest)
        {
            cam.LookAt(head);

            // Verifica se a animação terminou
            AnimatorStateInfo stateInfo = inimigoAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("inimigocena2") && stateInfo.normalizedTime >= 1f && !zoomRestored)
            {
                EndSequence();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            arrest = true;

            disable.DisablePlayer();
            inimigoAnimator.Play("inimigocena2");

            StartCoroutine(ZoomIn());
        }
    }

    private void EndSequence()
    {
        zoomRestored = true;
        arrest = false;

        texto.SetActive(true);
        disable.EnablePlayer();

        StartCoroutine(ZoomOut());
    }

    IEnumerator ZoomIn()
    {
        float startFOV = mainCamera.fieldOfView;
        float time = 0;

        while (time < zoomDuration)
        {
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, zoomFOV, time / zoomDuration);
            time += Time.deltaTime;
            yield return null;
        }

        mainCamera.fieldOfView = zoomFOV;
       
    }

    IEnumerator ZoomOut()
    {
       
        float startFOV = mainCamera.fieldOfView;
        float time = 0;

        while (time < zoomDuration)
        {
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, originalFOV, time / zoomDuration);
            time += Time.deltaTime;
            yield return null;
        }

        mainCamera.fieldOfView = originalFOV;
    }
}