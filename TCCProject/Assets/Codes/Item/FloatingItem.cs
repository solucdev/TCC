using UnityEngine;

public class FloatingItem : MonoBehaviour {
	[SerializeField] private float amplitude = 0.25f;
	[SerializeField] private float frequency = 1f;
	[SerializeField] private float rotationSpeed = 50f;
	private bool collected;

	private Vector3 startPos;
	private Quaternion startRot;

	void Start() {
		startPos = transform.position;
	}

	void Update() {
		if (LayerMask.LayerToName(gameObject.layer) == "Item" && !collected) {
			float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
			transform.position = new Vector3(startPos.x, newY, startPos.z);

			transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);	
		}
		else { collected = true; }
	}
}