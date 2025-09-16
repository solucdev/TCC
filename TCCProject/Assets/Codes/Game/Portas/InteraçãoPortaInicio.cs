using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteraçãoPortaInicio : MonoBehaviour
{
    public float distanciaMaxima = 5f;
    public LayerMask camadaPorta;
    public Text interacaoTexto;
    private Camera cameraJogador;

    void Start()
    {
        cameraJogador = Camera.main;
        interacaoTexto.text = "";
    }

    void Update()
    {
        Ray ray = new Ray(cameraJogador.transform.position, cameraJogador.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaMaxima, camadaPorta))
        {
            if (hit.collider.CompareTag("Porta"))
            {
                interacaoTexto.text = "Pressione E para interagir com a porta";
                return;
            }
        }

        interacaoTexto.text = "";
    }
}
