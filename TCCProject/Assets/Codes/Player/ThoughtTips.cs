using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class ThoughtTips : MonoBehaviour
{
	Image img;
	float duration = 1;
	[SerializeField] TypeWriter effect;
    [SerializeField] GameObject thotip;
	Text txt;
	IEnumerator Start() {
		txt = thotip.GetComponent<Text>();
		img = GetComponent<Image>();
		yield return new WaitForSeconds(4.9f);
		StartCoroutine(Thought("...tenho que sair deste lugar"));
	}

	public IEnumerator Thought(string message) {
		thotip.SetActive(true);
		StartCoroutine(FadeIn());
        StartCoroutine(effect.Typer(txt, message));
		yield return new WaitForSeconds(5);
		thotip.SetActive(false);
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
