using UnityEngine;

public class TimeBarKeys : MonoBehaviour {
	private QTEKeys bunchOK;
	public RectTransform bar;

	const float maxWidth = 72f;
	const float overflowWidth = 400f;

	float timing = 0f;
	int lastRound = 0;

	void Start() {
		bunchOK = GetComponent<QTEKeys>();
		bar.sizeDelta = new Vector2(maxWidth, bar.sizeDelta.y);
	}

	void Update() {
		if (bunchOK == null || !gameObject.activeSelf) return;

		if (bunchOK.round > 0 && bunchOK.round <= 8) {
			if (bunchOK.round != lastRound) {
				timing = 0f;
				bar.sizeDelta = new Vector2(maxWidth, bar.sizeDelta.y);
				lastRound = bunchOK.round;
			}

			timing += Time.deltaTime;
			float t = bunchOK.timer > 0f ? Mathf.Clamp01(timing / bunchOK.timer) : 1f;
			float w = Mathf.Lerp(maxWidth, 0f, t);
			bar.sizeDelta = new Vector2(w, bar.sizeDelta.y);

			if (t >= 1f - 0.0001f) {
				bar.sizeDelta = new Vector2(overflowWidth, bar.sizeDelta.y);
			}
		} else {
			ResetRound();
		}
	}

	public void ResetRound() {
		bar.sizeDelta = new Vector2(maxWidth, bar.sizeDelta.y);
		timing = 0f;
	}
}