using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFim : MonoBehaviour
{
    public string[] frases;
    public Text fraseFiller;
    public GameObject botaoMenu;
    private int index = 0;
    public GameObject botaoContinuar;

    void Start()
    {
        botaoMenu.SetActive(false);
        Continuar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continuar()
    {
        if (index < frases.Length)
        {
            fraseFiller.text = frases[index];
            index++;
        }
        else
        {
            fraseFiller.text = " ";
            botaoMenu.SetActive(true);
            botaoContinuar.SetActive(false);
        }
    }
}
