using UnityEngine;

public class CleanRender : MonoBehaviour {
	void Start() {
		// Pega todos os MeshRenderers da cena
		MeshRenderer[] renderers = FindObjectsOfType<MeshRenderer>();

		foreach (MeshRenderer mr in renderers) {
			// Desativa o renderer original
			mr.enabled = false;

			// Cria um cubo no mesmo lugar
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

			// Posiciona e ajusta o cubo
			cube.transform.position = mr.transform.position;
			cube.transform.rotation = mr.transform.rotation;
			cube.transform.localScale = mr.transform.lossyScale;

			// Remove o collider do cubo (se não quiser colisão extra)
			Destroy(cube.GetComponent<BoxCollider>());

			// Aplica material básico sem textura
			cube.GetComponent<Renderer>().material = new Material(Shader.Find("Standard"));
			cube.GetComponent<Renderer>().material.color = Color.gray;
		}
	}
}