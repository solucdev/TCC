using UnityEngine;
using UnityEngine.UI;

public class VolumeConfig : MonoBehaviour {
	public Slider volume;
	public Text ntext;

	void Start() {
		volume.minValue = 0;
		volume.maxValue = 100;
		volume.value = 50;
		volume.wholeNumbers = true;

		volume.onValueChanged.AddListener(delegate { AttValue(); });
		AttValue();
	}

	void AttValue() {
		ntext.text = volume.value.ToString();
		AudioListener.volume = volume.value / 100;
	}
}