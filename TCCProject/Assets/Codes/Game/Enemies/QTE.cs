using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class QTE : MonoBehaviour
{
    [SerializeField] GameObject qte;

    public List<KeyCode> KeyButtons = new List<KeyCode>();
    public List<Sprite> KeySprites = new List<Sprite>();
    [SerializeField] Image buttonPlace;
    [SerializeField] Image errorflash;
    [SerializeField] Transform head;
    [SerializeField] Animator fbx;

    public NavMeshMove ai;
    public Stun stun;
    public GameObject attack;
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
            stun.enabled = false;
            fbx.Play("right hook");
            fbx.Play("swagger");
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

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2);
        stun.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tsecs > timer && HasLineOfSight())
        {
            attack.SetActive(false);
            qte.SetActive(true);
            OnQTE();
            arrest = true;
        }
    }

    bool HasLineOfSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Raycast para verificar se há algo entre o inimigo e o jogador
        if (Physics.Raycast(transform.position, directionToPlayer.normalized, out RaycastHit hit, distanceToPlayer))
        {
            // Verifica se o que foi atingido é o jogador
            if (hit.collider.CompareTag("Player"))
            {
                return true; // Visão desobstruída
            }
            else
            {
                return false; // Algo está bloqueando a visão
            }
        }

        return false;
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
