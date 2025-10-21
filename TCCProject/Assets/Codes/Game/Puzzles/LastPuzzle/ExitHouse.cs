using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHouse : MonoBehaviour
{
    [SerializeField] Inventory inv;
    [SerializeField] GameObject keys;
    [SerializeField] GameObject i;
    [SerializeField] Transform camholder;
    [SerializeField] Disable system;
    [SerializeField] GameObject keymolde;
    private Transform cam;

    private int chances;

    void Start()
    {
        cam = camholder.GetComponentInChildren<Transform>();
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartExitPuzzle(); //para jogo apagar essa linha <<<
            if (inv.ItemInHand(keys))
            {
                StartExitPuzzle();
            }
            else
            {
                i.SetActive(true);
                StartCoroutine(Delay());
            }
        }
    }

    void StartExitPuzzle()
    {
        system.DisablePlayer();
        cam.rotation = Quaternion.Euler(0, 0, 0);
        camholder.position = new Vector3(148.733994f, 2.29500008f, 247.169998f);
        camholder.rotation = Quaternion.Euler(67.8154526f, 265.242645f, 355.204651f);
        keymolde.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        i.SetActive(false);
    }

    public void Right()
    {
        SceneManager.LoadScene("cena do final");
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Wrong()
    {
        if(chances <= 2)
        {
            chances++;
        }
        else
        {
			FindObjectOfType<PlayerDeathManager>().PlayerDied();
        }
    }
}

