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
    [SerializeField] Transform cam;
    [SerializeField] GameObject puzzle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
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
        keymolde.SetActive(true);
        camholder.position = new Vector3(149, 2.5f, 247.2f);
        cam.rotation = Quaternion.Euler(60, -90, 0);
        puzzle.SetActive(true);

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
}

