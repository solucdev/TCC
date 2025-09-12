using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] GameObject staminaobj;
    public RectTransform bar;
    [HideInInspector] public bool tired;
    public float timing = 0;
    float stamina = 5;
    float sizey;
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && !player.crouch.isdown)
        {
            SpendStamina();
            if(bar.sizeDelta.y <= 1)
            {
                tired = true;
            }
            else
            {
                tired = false;
            }
        }
        else { 
            Recovering(); }
    }

    void SpendStamina()
    {
        staminaobj.SetActive(true);
        timing += Time.deltaTime;
        float t = Mathf.Clamp01(timing / stamina);

        bar.sizeDelta = new Vector2(bar.sizeDelta.x, Mathf.Lerp(100, 0, t));
    }

    void Recovering()
    {
        staminaobj.SetActive(false);
        timing += Time.deltaTime;
        float t = Mathf.Clamp01(timing / 10f);

        bar.sizeDelta = new Vector2(bar.sizeDelta.x, Mathf.Lerp(0, 100, t));
        sizey = bar.sizeDelta.y;    
    }
}
