using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastPuzzle : MonoBehaviour
{

    public Button[] buttons; // Referência aos 6 botões
    public GameObject victoryScreen;
    public GameObject deathScreen;
    public GameObject puzzleScreen;

    private int correctButtonIndex;
    private int attemptsLeft = 3;
    private bool puzzleEnded = false;

    void Start()
    {
        correctButtonIndex = 2; // Escolhe botão correto aleatoriamente

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Necessário para capturar o índice corretamente no lambda
            buttons[i].onClick.AddListener(() => OnButtonPressed(index));
        }

        victoryScreen.SetActive(false);
        deathScreen.SetActive(false);
    }

    void OnButtonPressed(int index)
    {
        if (puzzleEnded) return;

        if (index == correctButtonIndex)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            puzzleEnded = true;
            victoryScreen.SetActive(true);
            puzzleScreen.SetActive(false);
            Debug.Log("Acertou!");
        }
        else
        {
            attemptsLeft--;
            Debug.Log("Errou! Tentativas restantes: " + attemptsLeft);

            if (attemptsLeft <= 0)
            {
                puzzleEnded = true;
                deathScreen.SetActive(true);
                puzzleScreen.SetActive(false);
                Debug.Log("Morreu!");
            }
        }
    }
}
