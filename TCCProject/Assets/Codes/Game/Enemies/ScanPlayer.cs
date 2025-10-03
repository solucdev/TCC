using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanPlayer : MonoBehaviour
{
    [SerializeField] LayerMask pLayer;
    [SerializeField] NavMeshMove ai;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & pLayer) != 0)
        {
            ai.FollowPlayer();
        }
    }
}
