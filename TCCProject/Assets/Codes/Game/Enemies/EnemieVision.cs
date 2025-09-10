using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieVision : MonoBehaviour
{
    [SerializeField] LayerMask pLayer;
    [SerializeField] Transform test;
    RaycastHit hit;
    int antibug;
    int mask;

    private void Start()
    {
        antibug = LayerMask.NameToLayer("Detector");
        mask = ~(1 << antibug);
    }
    private void Update()
    {   See();    }

    void See()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10, mask))
        {
            test.position = hit.point;
            Debug.DrawRay(transform.position, transform.forward * 10, Color.white);
        }
    }
}
