using UnityEngine;
using System.Collections.Generic;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> grassSteps;
    public float runMultiplier = 0.6f;
    public List<AudioClip> woodSteps;
    private Rigidbody rb;
    private bool isPlayingStep = false;
    private AudioClip lastClip;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (IsGrounded() && GetHorizontalSpeed() > 0.1f)
        {
            if (!isPlayingStep)
                StartCoroutine(PlayFootstepSequence());
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.down * 0.5f, Vector3.down, 1.5f);
    }

    float GetHorizontalSpeed()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        return horizontalVelocity.magnitude;
    }

    System.Collections.IEnumerator PlayFootstepSequence()
    {
        isPlayingStep = true;

        // RaycastAll para ignorar Player
        RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.down * 0.5f, Vector3.down, 3f);
        RaycastHit groundHit = default;
        foreach (var h in hits)
        {
            if (!h.collider.CompareTag("Player"))
            {
                groundHit = h;
                break;
            }
        }

        if (groundHit.collider == null)
        {
            isPlayingStep = false;
            yield break; // Não achou chão
        }

        Debug.Log("Tag detectada: " + groundHit.collider.tag);

        // Seleciona lista de clipes
        List<AudioClip> clips = null;
        if (groundHit.collider.CompareTag("Grass")) clips = grassSteps;
        else if (groundHit.collider.CompareTag("Wood")) clips = woodSteps;

        if (clips != null && clips.Count > 0)
        {
            // Escolhe um clipe diferente do último
            AudioClip clip;
            do
            {
                clip = clips[Random.Range(0, clips.Count)];
            } while (clip == lastClip && clips.Count > 1);

            lastClip = clip;

            // Toca som
            audioSource.PlayOneShot(clip);
            Debug.Log("Som tocado: " + clip.name);

            // Ajusta intervalo conforme velocidade
            float speedFactor = rb.velocity.magnitude > 3f ? runMultiplier : 1f;
            yield return new WaitForSeconds(clip.length * speedFactor);
        }

        isPlayingStep = false;
    }
}
