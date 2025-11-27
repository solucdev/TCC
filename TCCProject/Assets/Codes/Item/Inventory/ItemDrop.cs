using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {
	int antibug;
	int mask;
	[SerializeField] GameObject cam;
	private Vector3 drope;

	private void Start() {
		antibug = LayerMask.NameToLayer("Player");
		mask = ~(1 << antibug);
		drope = new Vector3(0, 0, 0);
	}

	public void Drop(GameObject itemdrop) {
		Vector3 origin = transform.position;
		RaycastHit hit;

		if (Physics.Raycast(origin, cam.transform.forward, out hit, 3, mask, QueryTriggerInteraction.Ignore)) {
			drope = hit.point + Vector3.up * 1f;
		} else {
			drope = transform.position + transform.forward;
		}

		Rigidbody rb = itemdrop.GetComponent<Rigidbody>();
		if (rb == null) {
			rb = itemdrop.AddComponent<Rigidbody>();
		}
		ItemStats script = itemdrop.GetComponent<ItemStats>();

		itemdrop.SetActive(true);
		itemdrop.transform.SetParent(null);

		itemdrop.transform.position = drope;
		itemdrop.transform.rotation = Quaternion.Euler(0, itemdrop.transform.rotation.y, 0);
		itemdrop.layer = LayerMask.NameToLayer("Item");
		rb.AddForce(Vector3.up);

		itemdrop.transform.localScale = new Vector3(itemdrop.transform.localScale.x / script.scale,
						itemdrop.transform.localScale.y / script.scale, itemdrop.transform.localScale.z / script.scale);

		StartCoroutine(CheckStabilize(rb));
	}

	IEnumerator CheckStabilize(Rigidbody rb) {
		yield return new WaitForSeconds(0.25f);
		float timer = 0f;
		while (timer < 5f) {
			if (rb.velocity.magnitude < 0.05f && rb.angularVelocity.magnitude < 0.05f) {
				Destroy(rb);
				yield break;
			}
			timer += Time.deltaTime;
			yield return null;
		}
		Destroy(rb);
	}
}