using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActScare : MonoBehaviour
{
    [SerializeField] GameObject poser;
    [SerializeField] AudioSource jumpscare;
    [SerializeField] Door door;
    [SerializeField] int probability;
    int index;

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			Randomizer();
		}
	}
	private void OnTriggerStay(Collider other) {

        if(index <= probability) {
			if (other.CompareTag("Player") && door.opened && door.elapsed > 0 && door.elapsed < 1) {
				Jumpscare();
				StartCoroutine(Disappear());
			}
            if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !door.opened) {
			jumpscare.Play();
			}
		}
	}

	void Jumpscare() {
			poser.SetActive(true);
	}

    void Randomizer() {
        index = Random.Range(1, 100);
    }

    IEnumerator Disappear() {
        yield return new WaitForSeconds(1);
        poser.SetActive(false);
    }
	IEnumerator Sound() {
		yield return new WaitForSeconds(3);
		jumpscare.Stop();
	}
}
