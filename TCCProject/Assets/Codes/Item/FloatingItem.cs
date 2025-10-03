using UnityEngine;

public class FloatingItem : MonoBehaviour {
	[SerializeField] float amplitude = 0.25f;
	[SerializeField] float frequency = 1f;
	[SerializeField] float rotationSpeed = 50f;

	Vector3 startPos;
	Quaternion startRot;

	void Start() {
		startPos = transform.position;
	}

	void Update() {
		if (LayerMask.LayerToName(gameObject.layer) == "Item") {
			float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
			transform.position = new Vector3(startPos.x, newY, startPos.z);

			transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
		} else {
			transform.position = startPos;
			transform.rotation = startRot;
		}
	}
}