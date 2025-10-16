using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineKey : MonoBehaviour {
	Outline outline;
	Transform ppos;
	GameObject player;
	public int detectionRadius;
	void Start() {
		outline = GetComponent<Outline>();
		player = GameObject.FindWithTag("Player");
		ppos = player.GetComponent<Transform>();
	}

	void Update() {
		if (Vector3.Distance(transform.position, ppos.position) <= detectionRadius && LayerMask.LayerToName(gameObject.layer) == "Default") {
			outline.enabled = true;
		} else {
			outline.enabled = false;
		}
	}
}
