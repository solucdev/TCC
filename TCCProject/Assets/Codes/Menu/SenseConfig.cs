using UnityEngine;
using UnityEngine.UI;

public class SenseConfig : MonoBehaviour {
	public Slider sense;
	public Text ntext;

	void Start() {
		sense.minValue = 10;
		sense.maxValue = 400;
		sense.value = 200;
		sense.wholeNumbers = true;

		sense.onValueChanged.AddListener(delegate { AttValue(); });
		AttValue();
	}

	void AttValue() {
		sense.value = Mathf.Round(sense.value / 5) * 5;
		ntext.text = sense.value.ToString();
	}
}