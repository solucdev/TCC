using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHands : MonoBehaviour
{
    public GameObject hands;
    float tmp;
    private void Update()
    {
        tmp += Time.deltaTime;
        if (tmp >= 12)
        {
            Destroy(hands);
        }
    }
}
