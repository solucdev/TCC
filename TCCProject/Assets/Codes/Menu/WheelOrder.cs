using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOrder : MonoBehaviour
{
    [SerializeField] GameObject selector;
    void Start()
    {
		selector.transform.SetAsLastSibling();
	}
    void Update()
    {
        
    }
}
