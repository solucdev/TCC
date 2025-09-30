using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {
	[SerializeField] LayerMask collects;
	[SerializeField] GameObject i;
	[SerializeField] Transform handfit;
	[SerializeField] float range;
	[SerializeField] Inventory inv;

	void Update() {
		RaycastHit hit;
		Vector3 direction = transform.forward;

		if (Physics.Raycast(transform.position, direction, out hit, range, collects)) {
			GameObject itemPrefab = hit.collider.gameObject;
			ItemStats script = itemPrefab.GetComponent<ItemStats>();

			if (script != null) {
				i.SetActive(true);
				script.ShowName();

				if (Input.GetMouseButtonDown(0)) {
					script.Feedback();
					StartCoroutine(ClearText(script));

					itemPrefab.layer = LayerMask.NameToLayer("MyItem");
					inv.AddItem(itemPrefab);
					itemPrefab.transform.localScale = new Vector3(itemPrefab.transform.localScale.x * 2,
						itemPrefab.transform.localScale.y * 2, itemPrefab.transform.localScale.z * 2);
					itemPrefab.SetActive(false);
				}
			}
		}
		else
		{
			i.SetActive(false);
		}
	}

	IEnumerator ClearText(ItemStats scr) {
		yield return new WaitForSeconds(2);
		scr.f.text = "";
	}
}
