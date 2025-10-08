using UnityEngine;

public class ArvoreDesapareceLonge : MonoBehaviour
{
    public float distanciaMaxima = 20f;
    private Transform jogador;
    private Renderer[] renderizadores;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        renderizadores = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jogador.position);
        bool estaLonge = distancia > distanciaMaxima;

        foreach (Renderer r in renderizadores)
        {
            r.enabled = !estaLonge;
        }
    }
}
