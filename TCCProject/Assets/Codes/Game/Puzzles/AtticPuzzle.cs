using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtticPuzzle : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject playercam;
    [SerializeField] GameObject i;
    [SerializeField] GameObject tdu;
    [SerializeField] Inventory inv;
    private bool clicked;
    private bool withItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "banquinho----------puzzle attic")
        {
            Debug.Log("banco na posição só subir");
            weapon.layer = LayerMask.NameToLayer("Item");
        }
    }

    private void Update()
    {
        if (clicked)
        {
            tdu.transform.position = Vector3.MoveTowards(tdu.transform.position, tdu.transform.right, 2 * Time.deltaTime); //SÓ TEMPORÁRIO, DPS TERÁ ANIMAÇÃO.
        }
        CheckInv();
        if (Physics.Raycast(playercam.transform.position, playercam.transform.forward, out RaycastHit hit, 3) && withItem)
        {
            GameObject trapdoor = hit.collider.gameObject;
            if (trapdoor == tdu)
            {
                Collect camcollect = playercam.GetComponent<Collect>();
                
                i.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    clicked = true;
                }
			}
		}
        else
        {
            i.SetActive(false);
        }
    }

    void CheckInv()
    {
        for (int i = 0; i < inv.inventory.Count; i++)
        {
            GameObject prefab = inv.inventory[i];
            ItemStats scripter = prefab.GetComponent<ItemStats>();

            if (prefab.activeSelf)
            {
                if (scripter.itemname == "Pé de Cabra")
                {
                    withItem = true;
                }
                else
                { withItem = false; }
            }
            else { withItem = false; }
        }
    }  
}