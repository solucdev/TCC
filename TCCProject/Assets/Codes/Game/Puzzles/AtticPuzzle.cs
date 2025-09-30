using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtticPuzzle : MonoBehaviour
{
    [SerializeField] GameObject bench;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject playercam;
	[SerializeField] GameObject i;
    [SerializeField] GameObject iplacer;
	[SerializeField] GameObject tdu;
    [SerializeField] Inventory inv;

    [SerializeField] Transform placePos;
    private bool clicked;
    private bool withPdCabra;
    private bool withBench;

    [SerializeField] float openSpeed = -125;
	private float currentAngle = 0;
	private bool opening = false;

	private void OnTriggerStay(Collider other) {
		if (other.CompareTag("Player") && withBench) {
			iplacer.SetActive(true);
			if (Input.GetKeyDown(KeyCode.F)) {
				inv.PlaceItem(bench, placePos);
				weapon.layer = LayerMask.NameToLayer("Item");
			}
		}
	}
	private void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			iplacer.SetActive(false );
		}
		}

	private void Update()
    {

		if (clicked && !opening) {
			opening = true;
			StartCoroutine(OpenTrapdoor());
		}
		withPdCabra = CheckInv("Pé de Cabra");
        withBench = CheckInv("Banquinho");
        if (Physics.Raycast(playercam.transform.position, playercam.transform.forward, out RaycastHit hit, 3) && withPdCabra)
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

	bool CheckInv(string ItemName) {
		for (int i = 0; i < inv.inventory.Count; i++) {
			GameObject prefab = inv.inventory[i];
			ItemStats scripter = prefab.GetComponent<ItemStats>();

			if (prefab.activeSelf && scripter.itemname == ItemName)
				return true;
		}
		return false;
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