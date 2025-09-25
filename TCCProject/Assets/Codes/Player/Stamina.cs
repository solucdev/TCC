using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] GameObject staminaobj;
    public RectTransform bar;
    float timing = 0;
    float stamina = 110;
    float sizey;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift)) {
            timing = 0;
        }
        if(Input.GetKey(KeyCode.LeftShift) && !player.crouch.isdown)
        {
            SpendStamina();
        }
        else { 
            Recovering(); 
        }
    }

    void SpendStamina()
    {
        staminaobj.SetActive(true);
        timing += Time.deltaTime;
        float t = Mathf.Clamp01(timing / stamina);

        bar.sizeDelta = new Vector2(bar.sizeDelta.x, Mathf.Lerp(bar.sizeDelta.y, 0, t));
    }

    void Recovering()
    {
        staminaobj.SetActive(false);
        timing += Time.deltaTime;
        float t = Mathf.Clamp01(timing / 1400);

        bar.sizeDelta = new Vector2(bar.sizeDelta.x, Mathf.Lerp(bar.sizeDelta.y, 100, t));
        sizey = bar.sizeDelta.y;    
    }

    public bool IsTired() {
        return bar.sizeDelta.y <= 1 ? true : false;
    }
}
