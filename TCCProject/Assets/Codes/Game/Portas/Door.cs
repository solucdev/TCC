
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] float angle = 90;
    private int speed = 2;
    public bool locked;
    public GameObject key;

    public float elapsed;

    public bool opened = false;
    Quaternion closedRotation;
    Quaternion openRotation;
    public string roomName;

    // Sons
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;
    private AudioSource audioSource;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, angle, 0));

        // Garante que existe um AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public IEnumerator ToggleDoor()
    {
        if (locked && key != null)
        {locked = false;}
        if (locked)
        {yield break;}
        Quaternion targetRotation;
        Quaternion startRotation = transform.rotation;

        if (opened)
        {
            targetRotation = closedRotation;
            PlaySound(closeSound);
        }
        else
        {
            targetRotation = openRotation;
            PlaySound(openSound);
        }
        opened = !opened;

        elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public bool IsOpen => opened;
}
