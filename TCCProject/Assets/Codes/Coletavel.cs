using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coletavel : MonoBehaviour
{
    public int totalColetaveis = 3; // Total necessário para completar
    private int coletados = 0; // Contador atual

    public TextMeshProUGUI textoContagem;
    public GameObject puzzleUI;

    void Start()
    {
        coletados = 0;
        puzzleUI.SetActive(false);
        AtualizarUI();
    }

    public void ColetarItem()
    {
            coletados++;
            AtualizarUI();

            // Ativa a UI se ainda não estiver ativa
            if (!puzzleUI.activeSelf)
            {
                puzzleUI.SetActive(true);
            }

            // Se coletou todos, pode ocultar a UI ou fazer outra ação
            if (coletados >= totalColetaveis)
            {
                puzzleUI.SetActive(false);
                // Aqui você pode chamar outra função, como abrir uma porta, etc.
            }
        
    }

    void AtualizarUI()
    {
        textoContagem.text = coletados.ToString() + "/" + totalColetaveis.ToString();
    }
}
