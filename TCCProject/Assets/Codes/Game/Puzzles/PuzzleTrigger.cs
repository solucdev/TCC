using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject puzzleUI;
    public Transform player;
    public float interactionDistance = 3f;
    public GameObject texto;

    private bool puzzleCompleted = false;

    void Update()
    {

        Ray ray = new Ray(player.position, player.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.transform == transform && !puzzleCompleted)
            {
                texto.SetActive(true); // Ativa o texto quando o jogador olha para o objeto

                if (Input.GetKeyDown(KeyCode.E))
                {
                    bool isActive = puzzleUI.activeSelf;
                    puzzleUI.SetActive(!isActive);

                    if (!isActive)
                    {
                        // Abrir puzzle
                        Time.timeScale = 0f;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                    else
                    {
                        // Fechar puzzle
                        Time.timeScale = 1f;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
                }
            }
            else
            {
                texto.SetActive(false); // Desativa se não estiver olhando para o objeto
            }
        }
        else
        {
            texto.SetActive(false); // Desativa se não houver hit
        }
    }


    public void CompletePuzzle()
    {
        puzzleCompleted = true;
        texto.SetActive(false);
        Objetivos.Instance.SetObjective("Abra a porta do acervo da seita");
    }

}