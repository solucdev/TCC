using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThoughtTips : MonoBehaviour
{
	Image img;
	float duration = 1;
	[SerializeField] TypeWriter effect;
    [SerializeField] GameObject thought1;
	Text text1;
    void Start()
    {
		text1 = thought1.GetComponent<Text>();
		img = GetComponent<Image>();
        StartCoroutine(Thought1());
    }

    IEnumerator Thought1() {
		yield return new WaitForSeconds(4.9f);
		thought1.SetActive(true);
		StartCoroutine(FadeIn());
        StartCoroutine(effect.Typer(text1));
		yield return new WaitForSeconds(5);
		thought1.SetActive(false);
		StartCoroutine(FadeOut());
	}



	IEnumerator FadeIn() {
		Color c = img.color;
		c.a = 0f;
		img.color = c;

		float t = 0f;
		while (t < duration) {
			t += Time.deltaTime;
			c.a = Mathf.Lerp(0f, 0.8f, t / duration);
			img.color = c;
			yield return null;
		}

		c.a = 0.8f;
		img.color = c;
	}
	IEnumerator FadeOut() {
		Color c = img.color;
		c.a = 0.8f;
		img.color = c;

		float t = 0f;
		while (t < duration) {
			t += Time.deltaTime;
			c.a = Mathf.Lerp(0.8f, 0f, t / duration);
			img.color = c;
			yield return null;
		}

		c.a = 0f;
		img.color = c;
	}
}
