using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject qte;
    [SerializeField] private AudioSource ambientSound;

    void Update()
    {
        HandlePauseInput();
    }

    private void HandlePauseInput()
    {
        if (UIManager.Instance != null && UIManager.Instance.IsAnyUIOpen && !UIManager.Instance.isPauseOpen)
            return;

        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && !qte.activeSelf)
        {
            if (settingsMenu.activeSelf)
            {
                // Fecha ajustes e volta para pausa
                settingsMenu.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else if (pauseMenu.activeSelf)
            {
                // Fecha tudo
                PauseOff();
            }
            else
            {
                // Abre pausa
                PauseOn();
            }
        }
    }

    public void PauseOn()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManager.Instance.isPauseOpen = true;


        if (ambientSound != null && ambientSound.isPlaying)
            ambientSound.Pause();

    }

    public void PauseOff()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UIManager.Instance.isPauseOpen = false;


        if (ambientSound != null)
            ambientSound.UnPause();

    }
}