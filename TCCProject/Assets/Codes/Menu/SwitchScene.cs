using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void SwitchScenes(string scene) {
        SceneManager.LoadScene(scene); 
            Time.timeScale = 1.0f; 
            Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
    }
}
