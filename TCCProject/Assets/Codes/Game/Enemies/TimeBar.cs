using System.Collections;
using UnityEngine;

public class TimeBar : MonoBehaviour {
	QTE enemie;
	public RectTransform bar;

	float timing = 0;
	bool top = false;
	bool coroutineStarted = false;

	void Start() {
		enemie = GetComponent<QTE>();
		bar.sizeDelta = new Vector2(400, bar.sizeDelta.y);
	}

	void Update() {
		if (enemie.onqte) {
			top = false;
			timing += Time.deltaTime;
			float t = Mathf.Clamp01(timing / enemie.timer);

			bar.sizeDelta = new Vector2(Mathf.Lerp(400, 0, t), bar.sizeDelta.y);

			if (!coroutineStarted) {
				StartCoroutine(Reset());
				coroutineStarted = true;
			}
		}

		if (top) {
			bar.sizeDelta = new Vector2(400, bar.sizeDelta.y);
			top = false;
		}
	}

	IEnumerator Reset() {
		yield return new WaitForSeconds(enemie.timer + 0.25f);
		top = true;
		timing = 0;
		coroutineStarted = false;
	}
}