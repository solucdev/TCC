using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypeWriter : MonoBehaviour {
	public float delay;

	public IEnumerator Typer(Text uiText, string message) {
		StringBuilder sb = new StringBuilder();
		uiText.text = "";

		foreach (char c in message) {
			sb.Append(c);
			uiText.text = sb.ToString();
			yield return new WaitForSeconds(delay);
		}
	}
	public IEnumerator EraseMessage(Text uiText, float delay)
    {

		Color c = uiText.color;
		c.a = 0.8f;
		uiText.color = c;

		float t = 0f;
		while (t < delay)
		{
			t += Time.deltaTime;
			c.a = Mathf.Lerp(0.8f, 0f, t / delay);
			uiText.color = c;
			yield return null;
		}

		c.a = 0f;
		uiText.color = c;

	}
}