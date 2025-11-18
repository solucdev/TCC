using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareTrigger : MonoBehaviour
{
    public Animator jumpscareAnimator;     // Animator do objeto do susto
    public string animationName = "jumspacre"; // Nome da animação
    //public AudioSource jumpscareSound;     // Som do susto
    public CameraShake cameraShake;        // Script de tremor de câmera
    public float scareDuration = 2f;       // Tempo antes de trocar a cena
    public string nextSceneName;           // Nome da próxima cena
    public GameObject text;
    public Disable disable;
    [SerializeField] Transform head;
    [SerializeField] Transform cam;
    private bool arrest = false;




    private bool hasTriggered = false;

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
            arrest = true;
            disable.DisablePlayer();
            text.SetActive(false);
            hasTriggered = true;
            StartCoroutine(TriggerJumpscare());
        }
    }

    IEnumerator TriggerJumpscare()
    {
        // Toca som
        /* if (jumpscareSound != null)
             jumpscareSound.Play();*/

        // Inicia animação
        if (jumpscareAnimator != null)
            jumpscareAnimator.Play(animationName);

        // Tremor de câmera
        if (cameraShake != null)
            cameraShake.StartShake();

        // Espera o tempo do susto
        yield return new WaitForSeconds(scareDuration);

        // Troca de cena
        SceneManager.LoadScene(nextSceneName);
    }
}