using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public bool isPuzzleOpen = false;
    public bool isInventoryOpen = false;
    public bool isPauseOpen = false;
    public bool isVideoOpen = false;
    public bool isCartaOpen = false;

    public bool IsAnyUIOpen => isPuzzleOpen || isInventoryOpen || isPauseOpen || isVideoOpen || isCartaOpen;



    void Awake()
    {
        Instance = this;
    }



    public void UpdateTimeScale()
    {
        Time.timeScale = IsAnyUIOpen ? 0f : 1f;
    }

}