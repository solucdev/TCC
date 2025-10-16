using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public string[] sentences;
    private int index = 0;
    public DialogoAcervo cutscene;

    void Start()
    {
        ShowNextSentence();
    }

    public void ShowNextSentence()
    {
        if (index < sentences.Length)
        {
            dialogueText.text = sentences[index];
            index++;
        }
        else
        {
            cutscene.EndCutscene();
        }
    }
}