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
        if (Input.GetKeyDown(KeyCode.E) && !puzzleCompleted)
        {
            Vector3 direction = transform.position - player.position;
            if (direction.magnitude <= interactionDistance)
            {
                Ray ray = new Ray(player.position, player.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, interactionDistance))
                {
                    if (hit.transform == transform)
                    {
                        puzzleUI.SetActive(true);
                        Time.timeScale = 0f;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        texto.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        texto.SetActive(false);
    }


    public void CompletePuzzle()
    {
        puzzleCompleted = true;
    }

}