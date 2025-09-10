using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeStart : MonoBehaviour {
	private Image fadeImage;
	[SerializeField] float fadeDuration;

	void Start() {
		fadeImage = GetComponent<Image>();
		StartCoroutine(FadeOutCoroutine());
	}

	void Update()
	{
		if(fadeImage.color.a == 0)
		{
			Destroy(gameObject);
		}
	}

	public IEnumerator FadeOutCoroutine() {
		float t = 0f;
		Color c = fadeImage.color;

		while (t < fadeDuration) {
			t += Time.deltaTime;
			float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
			fadeImage.color = new Color(c.r, c.g, c.b, alpha);
			yield return null;
		}
	}
}
