using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    public Camera cam;
    public float range = 10f;
    private Inventory inv;
    public GameObject[] weapons;
    public GameObject attackIndicator;

    private void Start()
    {
        inv = GetComponent<Inventory>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 direction = cam.transform.forward;
        bool canStunTarget = false;

        if (Physics.Raycast(cam.transform.position, direction, out hit, range))
        {
            StunEffect targetStunEffect = hit.collider.GetComponentInChildren<StunEffect>();

            if (targetStunEffect != null)
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    if (inv.ItemInHand(weapons[i]))
                    {
                        canStunTarget = true;

                        if (Input.GetMouseButton(0))
                        {
                            WeaponAction(weapons[i], i);
                            targetStunEffect.DisableEnemy();
                        }
                    }
                }
            }
        }


        if (attackIndicator != null)
        {
            attackIndicator.SetActive(canStunTarget);
        }
    }

    void WeaponAction(GameObject item, int index)
    {
        if (index == 0)
        {
            Quaternion target = Quaternion.LookRotation(Vector3.right * 60, Vector3.up);
            item.transform.rotation = Quaternion.RotateTowards(item.transform.rotation, target, 3 * Time.deltaTime);
        }
        if (index == 1)
        {
            item.transform.position += item.transform.forward * 3 * Time.deltaTime;
        }
    }
}
