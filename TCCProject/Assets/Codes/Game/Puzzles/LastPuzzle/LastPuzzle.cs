using UnityEngine;
using UnityEngine.UI;
public class LastPuzzle : MonoBehaviour
{
    public GameObject[] keys;
    public GameObject victoryScreen;
    public GameObject deathScreen;
    public GameObject puzzleScreen;

    public Text[] numbers;

    private int correctButtonIndex;
    private int attemptsLeft = 3;
    private bool puzzleEnded = false;

    void Start()
    {
        Time.timeScale = 0f;
        correctButtonIndex = 4;
    }

    public void Press(int index)
    {
        if (puzzleEnded) return;

        if (index == correctButtonIndex)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            puzzleEnded = true;
            victoryScreen.SetActive(true);
            puzzleScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            keys[index].GetComponent<Rigidbody>().AddForce(Vector3.up * 800);
            attemptsLeft--;
            numbers[index].color = Color.red;

            if (attemptsLeft <= 0)
            {
                puzzleEnded = true;
                deathScreen.SetActive(true);
                puzzleScreen.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
