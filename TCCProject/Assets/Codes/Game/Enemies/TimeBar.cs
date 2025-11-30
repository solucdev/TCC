using System.Collections;
using UnityEngine;

public class TimeBar : MonoBehaviour {
	QTEPure enemie;
	public RectTransform bar;

	float timing = 0;
	bool top = false;
	bool coroutineStarted = false;

	void Start() {
		enemie = GetComponent<QTEPure>();
		bar.sizeDelta = new Vector2(72, bar.sizeDelta.y);
	}

	void Update() {
		if (enemie.onQTE) {
			top = false;
			timing += Time.deltaTime;
			float t = Mathf.Clamp01(timing / enemie.timer);

			bar.sizeDelta = new Vector2(Mathf.Lerp(72, 0, t), bar.sizeDelta.y);

			if (!coroutineStarted) {
				StartCoroutine(Reset());
				coroutineStarted = true;
			}
		} else {
			// Se o QTE terminou, reseta barra e estado
			timing = 0;
			coroutineStarted = false;
			bar.sizeDelta = new Vector2(72, bar.sizeDelta.y);
		}

		if (top) {
			bar.sizeDelta = new Vector2(400, bar.sizeDelta.y);
			top = false;
		}
	}

	IEnumerator Reset() {
		yield return new WaitForSeconds(enemie.timer + 0.25f);

		// Só reseta se o QTE ainda estiver ativo
		if (enemie.onQTE) {
			top = true;
			timing = 0;
		}

		coroutineStarted = false;
	}
	public void ResetRound() {
		timing = 0;
		bar.sizeDelta = new Vector2(72, bar.sizeDelta.y);
	}

}