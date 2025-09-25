using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPortas : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;
    public Transform jogador;
    public float distanciaMaxima = 3f;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;

    public bool estaTrancada = false;
    public bool jogadorTemChave = false;
    public GameObject chaveNecessaria;

    private Inventory inventarioJogador;

    void Start()
    {
        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
        inventarioJogador = jogador.GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distancia = Vector3.Distance(jogador.position, transform.position);

            if (distancia <= distanciaMaxima)
            {
                if (estaTrancada)
                {
                    if (inventarioJogador != null && inventarioJogador.TemItem(chaveNecessaria))
                    {
                        estaTrancada = false;
                        Debug.Log("Você usou a chave correta para destrancar a porta.");
                    }
                    else
                    {
                        Debug.Log("A porta está trancada. Você precisa da chave: " + chaveNecessaria);
                        return;
                    }
                }
                else if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(ToggleDoor());
            }
            else
            {
                Debug.Log("Você está muito longe da porta para interagir.");
            }
        }
    }

    IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? _closedRotation : _openRotation;
        isOpen = !isOpen;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
