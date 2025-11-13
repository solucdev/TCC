using UnityEngine;
using System.Collections.Generic;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> grassSteps;
    public List<AudioClip> woodSteps;
    public float baseStepInterval = 0.5f; // intervalo base para velocidade normal
    public float runMultiplier = 0.6f;    // reduz intervalo quando corre

    private Rigidbody rb;
    private bool isPlayingStep = false;
    private AudioClip lastClip;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsGrounded() && rb.velocity.magnitude > 0.1f)
        {
            if (!isPlayingStep)
                StartCoroutine(PlayFootstepSequence());
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.down * 0.5f, Vector3.down, 1.5f, ~LayerMask.GetMask("Player"));
    }

    System.Collections.IEnumerator PlayFootstepSequence()
    {
        isPlayingStep = true;

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.down * 0.5f, Vector3.down, out hit, 2f, ~LayerMask.GetMask("Player")))
        {
            List<AudioClip> clips = null;
            if (hit.collider.CompareTag("Grass")) clips = grassSteps;
            else if (hit.collider.CompareTag("Wood")) clips = woodSteps;

            if (clips != null && clips.Count > 0)
            {
                AudioClip clip;
                do
                {
                    clip = clips[Random.Range(0, clips.Count)];
                } while (clip == lastClip && clips.Count > 1);

                lastClip = clip;
                audioSource.PlayOneShot(clip);

                // Ajusta intervalo conforme velocidade
                float speedFactor = rb.velocity.magnitude > 3f ? runMultiplier : 1f;
                yield return new WaitForSeconds(clip.length * speedFactor);
            }
        }

        isPlayingStep = false;
    }
}