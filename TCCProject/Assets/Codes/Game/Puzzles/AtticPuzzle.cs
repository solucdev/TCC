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

    [SerializeField] float openSpeed = -125;
	private float currentAngle = 0;
	private bool opening = false;

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
		if (clicked && !opening) {
			opening = true;
			StartCoroutine(OpenTrapdoor());
		}
		CheckInv();
        if (Physics.Raycast(playercam.transform.position, playercam.transform.forward, out RaycastHit hit, 3) && withItem)
        {
            Debug.DrawRay(playercam.transform.position, playercam.transform.forward * 3, Color.yellow);
            GameObject trapdoor = hit.collider.gameObject;
            if (trapdoor.name == "trapdoor")
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
	IEnumerator OpenTrapdoor() {
		while (currentAngle < 90) {
			float step = openSpeed * Time.deltaTime;
			tdu.transform.Rotate(Vector3.left, step, Space.Self);
			currentAngle += step;
			yield return null;
		}
	}
}