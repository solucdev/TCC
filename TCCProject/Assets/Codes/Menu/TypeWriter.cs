using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypeWriter : MonoBehaviour {

	private string fullText;
	public float delay = 0.05f;

	public IEnumerator Typer(Text uiText) {
		fullText = uiText.text;
		uiText.text = "";
		foreach (char c in fullText) {
			uiText.text += c;
			yield return new WaitForSeconds(delay);
		}
	}

}