using UnityEngine;
using UnityEngine.UI;

public class GamaConfig : MonoBehaviour {
	public Slider gama;
	public Text ntext;
	public Image gamaPanel;

	void Start() {
		gama.minValue = 0;
		gama.maxValue = 100;
		gama.value = 50;
		gama.wholeNumbers = true;

		gama.onValueChanged.AddListener(delegate { AttValue(); });
		AttValue();
	}

	void AttValue() {
		ntext.text = gama.value.ToString();
		gamaPanel.color = new Color(0, 0, 0, 1 - gama.value / 100);
	}
}