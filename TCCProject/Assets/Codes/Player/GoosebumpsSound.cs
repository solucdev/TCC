using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoosebumpsSound : MonoBehaviour {
	public AudioSource audio;
	public Transform player;
	public float distanceTrigger = 5;
	public int cooldown = 60;

	private float lastPlayTime = -Mathf.Infinity;

	void Update() {
		float distance = Vector3.Distance(transform.position, player.position);

		if (distance <= distanceTrigger && Time.time - lastPlayTime >= cooldown) {
			audio.Play();
			lastPlayTime = Time.time;
		}
	}
}