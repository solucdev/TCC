using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomKeysItem : MonoBehaviour
{
	int range = 8;
	bool saw;
	[SerializeField] Disable disable;
	[SerializeField] QuadVentory invset;
	[SerializeField] ThoughtTips tip;
	[SerializeField] TypeWriter effect;

	[SerializeField] Text thought;
	[SerializeField] Camera cam;
	[SerializeField] Transform bunchK;
    void Start()
    {
    }

    void Update()
    {
		RaycastHit hit;
		Vector3 direction = cam.transform.forward;

		if (Physics.Raycast(cam.transform.position, direction, out hit, range)) {
			GameObject enemy = hit.collider.gameObject;
			if (enemy.name == "BodyEn" && !saw) {
				disable.DisablePlayer();
				invset.enabled = false;
				StartCoroutine(effect.Typer(thought, "...ali deve estar a chave da porta da frente"));
				StartCoroutine(effect.EraseMessage(thought, 5));
				//StartCoroutine(tip.Thought("")); ta todo bugado esse thought
				StartCoroutine(Zoom());
			}
		}
	}

	IEnumerator Zoom() {
		cam.transform.rotation = Quaternion.LookRotation(bunchK.position - cam.transform.position);
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 5, Time.deltaTime * 10);
		yield return new WaitForSeconds(5);
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 75, Time.deltaTime * 3);
		disable.EnablePlayer();
		invset.enabled = true;
		saw = true;
	}
}
