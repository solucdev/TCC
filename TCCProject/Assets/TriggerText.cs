using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerText : MonoBehaviour
{
    public GameObject player;
    public GameObject texto1;
    public GameObject texto2;


    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        texto1.SetActive(false);
        texto2.SetActive(true);
    }
}
