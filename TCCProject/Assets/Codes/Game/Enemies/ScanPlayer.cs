using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanPlayer : MonoBehaviour
{
    [SerializeField] LayerMask pLayer;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == pLayer)
        {
            Debug.Log("aaa viu o playre");
        }

    }
}
