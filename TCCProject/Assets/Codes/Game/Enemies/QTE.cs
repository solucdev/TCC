using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class QTE : MonoBehaviour
{
    [SerializeField] GameObject qte;

    public List<KeyCode> KeyButtons = new List<KeyCode>();
    public List<Sprite> KeySprites = new List<Sprite>();
    [SerializeField] Image buttonPlace;
    [SerializeField] Image errorflash;
    [SerializeField] Transform head;

    public float timer;
    private float startime;
    private float tsecs;
    private KeyCode stringkey;
   [HideInInspector] public bool onqte = false;

    private void Start()
    {
        startime = timer;
    }
    private void Update()
    {
        tsecs += Time.deltaTime;

        if (arrest)
        {
            ArrestPlayer();
        }

        if (Input.GetKeyDown(stringkey) && tsecs < timer && onqte)
        {
            Debug.Log("legal");
            arrest = false;
            disable.EnablePlayer();
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            qte.SetActive(false);
            onqte = false;
        }
        if (tsecs > timer && onqte)
        {
            Debug.Log("perdeu o qte");
            timer = startime;
            qte.SetActive(false);
            onqte = false;
            FindObjectOfType<PlayerDeathManager>().PlayerDied();
        }
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A)
             && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.LeftShift)
              && !Input.GetKeyDown(KeyCode.LeftControl) && !Input.GetKeyDown(stringkey) && tsecs < timer && onqte)
        {
            PiFlash();
            timer -= 1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tsecs > timer)
        {
            qte.SetActive(true);
            OnQTE();
            arrest = true;
        }
    }

    void OnQTE()
    {
        onqte = true;
        tsecs = 0;

        int button = Random.Range(0, KeyButtons.Count);
        stringkey = KeyButtons[button];
        buttonPlace.sprite = KeySprites[button];
    }
    public void PiFlash()
    {
        errorflash.color = new Color(1, 0, 0, 0.5f);
        Invoke(nameof(ClearPF), 0.1f);
    }

    void ClearPF()
    {
        errorflash.color = new Color(1, 1, 1, 1f);
    }

    [SerializeField] Transform cam;
    [SerializeField] Transform player;
    public Disable disable;
    private bool arrest;

    void ArrestPlayer()
    {
        disable.DisablePlayer();
        cam.rotation = Quaternion.LookRotation(head.position - cam.position);
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void ResetArrest()
    {
        arrest = false;
    }
}
