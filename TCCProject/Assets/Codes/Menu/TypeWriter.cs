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
}