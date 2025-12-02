using UnityEngine;

public class TimeBar : MonoBehaviour {
	private QTEPure enemie; // referência atual do QTE
	public RectTransform bar;

	const float maxWidth = 72f; // largura inicial
	const float overflowWidth = 400f; // efeito "top"

	void Start() {
		bar.sizeDelta = new Vector2(maxWidth, bar.sizeDelta.y);
	}

	void Update() {
		if (enemie == null || !gameObject.activeSelf) return;

		if (enemie.onQTE) {
			// Se a UI não deve pausar, use Time.unscaledDeltaTime e derive progress do QTE
			float t = enemie.timer > 0f ? Mathf.Clamp01(enemie.elapsed / enemie.timer) : 1f;
			// Barra diminuindo ao longo do tempo
			float w = Mathf.Lerp(maxWidth, 0f, t);
			bar.sizeDelta = new Vector2(w, bar.sizeDelta.y);

			// Efeito “top” quando estourar tempo (t ~ 1). Evita piscadas com threshold.
			if (t >= 1f - 0.0001f) {
				bar.sizeDelta = new Vector2(overflowWidth, bar.sizeDelta.y);
			}
		} else {
			// QTE terminou: reset visual
			ResetRound();
		}
	}

	public void Begin(QTEPure enemie) {
		this.enemie = enemie;
		ResetRound();
	}

	public void ResetRound() {
		bar.sizeDelta = new Vector2(maxWidth, bar.sizeDelta.y);
	}
}