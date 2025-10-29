using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objetivos : MonoBehaviour
{
    public static Objetivos Instance;

    [SerializeField] Text messageText; // ou Text se não usar TextMeshPro
    [SerializeField] float messageDuration = 4f;

    Queue<string> messageQueue = new Queue<string>();
    bool isShowingMessage = false;

    void Awake()
    {
        Instance = this;
        messageText.text = "";
    }


    public void SetObjective(string msg)
    {
        messageText.text = msg;
    }


    /*public void ShowMessage(string msg)
    {
        messageQueue.Enqueue(msg);
        if (!isShowingMessage) StartCoroutine(DisplayMessages());
    }

    IEnumerator DisplayMessages()
    {
        isShowingMessage = true;

        while (messageQueue.Count > 0)
        {
            messageText.text = messageQueue.Dequeue();
            yield return new WaitForSeconds(messageDuration);
            messageText.text = "";
        }

        isShowingMessage = false;
    }*/
}