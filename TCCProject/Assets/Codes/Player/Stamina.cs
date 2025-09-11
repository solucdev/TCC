using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    [SerializeField] GameObject staminaobj;
    public RectTransform bar;
    float timing = 0;
    float stamina = 5;

    void Update()
    {
        if(player.speed == player.spdrun)
        {
            SpendStamina();
        }
        else { staminaobj.SetActive(false); }
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
        timing += Time.deltaTime;
        float t = Mathf.Clamp01(timing / 10f);

        bar.sizeDelta = new Vector2(bar.sizeDelta.x, Mathf.Lerp(0, 100, t));
    }
}
