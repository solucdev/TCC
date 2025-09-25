using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShorthandReset : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.Alpha7) && Input.GetKey(KeyCode.L)) {
            SceneManager.LoadScene("WithModels");
        }
    }
}
