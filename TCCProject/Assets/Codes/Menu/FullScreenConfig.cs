using UnityEngine;
using UnityEngine.UI;

public class FullScreenConfig : MonoBehaviour {
	public Toggle toggle;

	void Start() {
		toggle.isOn = Screen.fullScreen;
		toggle.onValueChanged.AddListener(SetFullScreen);
	}
	void SetFullScreen(bool isFull) {
		Screen.fullScreen = isFull;
	}
}