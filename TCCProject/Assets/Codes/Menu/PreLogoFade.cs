using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreLogoFade : MonoBehaviour
{
	private Image fadeImage;
	[SerializeField] float fadeDuration;
	float time;
	bool started;

	private void Start() {
		fadeImage = GetComponent<Image>();
	}
	private void Update() {
		time += Time.deltaTime;
		if(time >= 7 && !started) {
			started = true;
			StartCoroutine(FadeInCoroutine());
		}
	}

	public IEnumerator FadeInCoroutine() {
		float t = 0f;
		Color c = fadeImage.color;

		while (t < fadeDuration) {
			t += Time.deltaTime;
			float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
			fadeImage.color = new Color(c.r, c.g, c.b, alpha);
			yield return null;
		}
		yield return new WaitForSeconds(fadeDuration);
		SceneManager.LoadScene("MenuKinematic");
	}
}
