using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    public Camera cam;
    public float range;
    private Inventory inv;
    public GameObject[] weapons;
    public GameObject attack;
    public StunEffect stun;

    private void Start()
    {
        inv = GetComponent<Inventory>();
    }
    void Update()
    {
        RaycastHit hit;
        Vector3 direction = cam.transform.forward;

        if (Physics.Raycast(cam.transform.position, direction, out hit, range))
        {
            if (hit.collider.gameObject.name == "fbx do mixamo")
            {
                if(inv.ItemInHand(weapons[0]) || inv.ItemInHand(weapons[1]) || inv.ItemInHand(weapons[2]))
                {
                    attack.SetActive(true);
                    if (Input.GetMouseButton(0))
                    {
                        //diminuir rotation x
                        stun.DisableEnemy();
                    }
                }
                else
                {
                    attack.SetActive(false);
                }
            }
        }
    }
}
