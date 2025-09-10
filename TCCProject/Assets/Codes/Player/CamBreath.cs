using UnityEngine;

public class CamBreath : MonoBehaviour {
	public float amplitude;
	public float frequency;

	private Vector3 initialPos;

	void Start() {
		initialPos = transform.localPosition;
	}
	void Update() {
			float offsetY = Mathf.Sin(Time.time * frequency) * amplitude;
			float offsetX = Mathf.Cos(Time.time * frequency * 0.5f) * amplitude * 0.5f;
			transform.localPosition = initialPos + new Vector3(offsetX, offsetY, 0f);
	}

}
