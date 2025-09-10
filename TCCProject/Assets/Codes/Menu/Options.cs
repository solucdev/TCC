using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public UnityEngine.GameObject menu;
    public UnityEngine.GameObject configs;
    public void ClickConfig() {
        menu.SetActive(false);
        configs.SetActive(true);
    }
    public void OffConfig() {
        menu.SetActive(true);
        configs.SetActive(false);
    }

    public void ToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitGame() {
        Application.Quit();
    }
}
