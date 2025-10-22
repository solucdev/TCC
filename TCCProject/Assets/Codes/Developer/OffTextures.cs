using UnityEngine;

public class OffTextures : MonoBehaviour {
	void Start() {
		Renderer[] renderers = FindObjectsOfType<Renderer>();
		foreach (Renderer r in renderers) {
			foreach (Material m in r.materials) {
				m.mainTexture = null;
			}
		}
	}
}