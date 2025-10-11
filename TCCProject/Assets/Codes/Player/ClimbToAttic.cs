using System.Collections;
using UnityEngine;

public class ClimbToAttic : MonoBehaviour {
	[SerializeField] GameObject player;
	[SerializeField] string animname;
	Animator anim;
	bool isPlaying;

	void Start() {
		anim = player.GetComponent<Animator>();
		anim.enabled = false;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player") && !isPlaying) {
			anim.enabled = true;
			isPlaying = true;
			anim.Play(animname, 0, 0f);
			StartCoroutine(WaitToFinish());
		}
	}

	IEnumerator WaitToFinish() {
		yield return null;
		float dur = anim.GetCurrentAnimatorStateInfo(0).length;
		yield return new WaitForSeconds(dur);
		isPlaying = false;
		anim.enabled = false;
	}
}