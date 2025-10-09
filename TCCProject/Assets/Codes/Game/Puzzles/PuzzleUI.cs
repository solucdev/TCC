using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{
    public InputField inputField;
    public GameObject puzzlePanel;
    //public GameObject keyPrefab;
    public GameObject keyObject;
    public Transform keySpawnPoint;
    public string correctAnswer = "sub luna";
    public PuzzleTrigger puzzleTrigger;

    public void CheckAnswer()
    {
        if (inputField.text.ToLower() == correctAnswer.ToLower())
        {
            puzzlePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Instantiate(keyPrefab, keySpawnPoint.position, Quaternion.identity);
            keyObject.SetActive(true);
            puzzleTrigger.CompletePuzzle();            
            Time.timeScale = 1f;
        }
    }
}