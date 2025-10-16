using System.Collections;
using UnityEngine;

public class TimeBarKeys : MonoBehaviour {
	QTEKeys bunchOK;
	public RectTransform bar;

	float timing = 0;
	int lastRound = 0;

	void Start() {
		bunchOK = GetComponent<QTEKeys>();
		bar.sizeDelta = new Vector2(72, bar.sizeDelta.y);
	}

	void Update() {
		if (bunchOK.round > 0 && bunchOK.round <= 7) {

			if (bunchOK.round != lastRound) {
				timing = 0;
				bar.sizeDelta = new Vector2(72, bar.sizeDelta.y);
				lastRound = bunchOK.round;
			}

			timing += Time.deltaTime;
			float t = Mathf.Clamp01(timing / bunchOK.timer);
			bar.sizeDelta = new Vector2(Mathf.Lerp(72, 0, t), bar.sizeDelta.y);
		}
	}
}