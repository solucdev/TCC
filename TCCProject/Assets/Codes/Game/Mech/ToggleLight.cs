using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    Light lighte;
	private void Start() {
        lighte = GetComponent<Light>();
	}
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && lighte.enabled) {
            lighte.enabled = false;
        }
        else if(Input.GetKeyDown(KeyCode.F) && !lighte.enabled) {
			lighte.enabled = true;
		}
    }
}
