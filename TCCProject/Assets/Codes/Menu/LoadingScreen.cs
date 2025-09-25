using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {
	[SerializeField] GameObject loadingUI;
	[SerializeField] Slider progressBar;

	public void LoadScene(string sceneName) {
		loadingUI.SetActive(true);
		StartCoroutine(LoadAsync(sceneName));
	}

	IEnumerator LoadAsync(string sceneName) {
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		operation.allowSceneActivation = false;

		while (!operation.isDone) {
			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			progressBar.value = progress;

			if (operation.progress >= 0.9f) {
				operation.allowSceneActivation = true;
			}

			yield return null;
		}
	}
}