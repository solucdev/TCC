using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtticPuzzleFix : MonoBehaviour {
	[SerializeField] GameObject i;
	[SerializeField] Transform bench;
	[SerializeField] ItemDrop drop;
	public bool atticset;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			i.SetActive(true);
		}
	}
	private void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			i.SetActive(false);
		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.F) && i.activeSelf) {
			atticset = true;
			bench.position = new Vector3(bench.position.x, bench.position.y, -100);
		}
	}
}