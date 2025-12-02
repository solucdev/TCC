using System.Collections;
using UnityEngine;

public class ActScare : MonoBehaviour {
	[SerializeField] GameObject poser;
	[SerializeField] AudioSource jumpscare;
	[SerializeField] Door door;

	[Header("Configuração de susto")]
	[SerializeField] int orderScare;
	[SerializeField] float cooldown;

	int currentTriggerCount = 0;
	bool onCooldown = false;



	private void OnTriggerStay(Collider other) {
		if (currentTriggerCount >= orderScare && !onCooldown) {
			if (other.CompareTag("Player") && door.opened && door.elapsed > 0 && door.elapsed < 1) {
				Jumpscare();
				StartCoroutine(Disappear());
				CountTrigger();
				StartCoroutine(CooldownRoutine());
			}
			if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !door.opened) {
				jumpscare.Play();
				StartCoroutine(Sound());
			}
		}
		if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !door.opened) {
			CountTrigger();
		}
	}

	void Jumpscare() {
		poser.SetActive(true);
	}

	void CountTrigger() {
		currentTriggerCount++;
	}

	IEnumerator Disappear() {
		yield return new WaitForSeconds(1);
		poser.SetActive(false);
	}

	IEnumerator Sound() {
		yield return new WaitForSeconds(3);
		jumpscare.Stop();
	}

	IEnumerator CooldownRoutine() {
		onCooldown = true;
		yield return new WaitForSeconds(cooldown);
		onCooldown = false;
		currentTriggerCount = 0;
	}
}