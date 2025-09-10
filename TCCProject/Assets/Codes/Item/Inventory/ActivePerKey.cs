using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePerKey : MonoBehaviour
{
    Inventory inv;
    void Start()
    {
        inv = GetComponent<Inventory>();   
    }

    void Update()
    {
		for (int i = 0; i <= 8; i++) {
			if (Input.GetKeyDown(KeyCode.Alpha0 + i))
			{
                inv.ActiveItem(i-1);
			}
		}

	}
}
