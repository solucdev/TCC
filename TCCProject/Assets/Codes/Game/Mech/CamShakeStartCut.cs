using UnityEngine;
using System.Collections;

public class CamShakeStartCut : MonoBehaviour {
	[SerializeField] MonoBehaviour[] updscripts; 
	[SerializeField] float cuttime;

	void Start() {
		StartCoroutine(DisableScripts());
		GetComponent<Animator>().Play("cam_shake");
	}

	IEnumerator DisableScripts() {
		foreach (var s in updscripts)
			s.enabled = false;

		yield return new WaitForSeconds(cuttime);

		foreach (var s in updscripts)
			s.enabled = true;

		Objetivos.Instance.SetObjective("Explore o ambiente e arrume um jeito de sair do sótão.");
	}


}