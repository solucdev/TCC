using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject screen;
    [SerializeField] GameObject qte;
    void Update()
    {
        PauseOn();
    }

    private void PauseOn()
    {
        if (UIManager.Instance != null && UIManager.Instance.IsAnyUIOpen && !UIManager.Instance.isPauseOpen)
            return;


        if (Input.GetKeyDown(KeyCode.P) && !qte.activeSelf || Input.GetKeyDown(KeyCode.Escape) && !qte.activeSelf)
        {
            Time.timeScale = 0f;
            screen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            UIManager.Instance.isPauseOpen = true;        }
    }
    public void PauseOff()
    {
            screen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            UIManager.Instance.isPauseOpen = false;    }
}

