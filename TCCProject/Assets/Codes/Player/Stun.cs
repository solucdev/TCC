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
                for(int i = 0; i < weapons.Length; i++)
                {
                    if (inv.ItemInHand(weapons[i]))
                    {
                        attack.SetActive(true);
                        if (Input.GetMouseButton(0))
                        {
                            WeaponAction(weapons[i], i);
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

    void WeaponAction(GameObject item, int index)
    {
        if (index == 0 )
        {
            Quaternion targt = Quaternion.LookRotation(Vector3.right * 60, Vector3.up);
            item.transform.rotation = Quaternion.RotateTowards(transform.rotation, targt, 3 * Time.deltaTime);
        }
        if(index == 1)
        {
            item.transform.position = Vector3.MoveTowards(item.transform.position, item.transform.forward, 3 * Time.deltaTime);
        }
    }
    }
