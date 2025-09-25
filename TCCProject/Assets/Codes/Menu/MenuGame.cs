using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuGame : MonoBehaviour, IPointerEnterHandler {
	[SerializeField] RectTransform btjogar;
	[SerializeField] RectTransform btajustes;
	[SerializeField] RectTransform btsair;

	[SerializeField] RectTransform bjogar;
	[SerializeField] RectTransform bsair;
	[SerializeField] int index;

	public void OnPointerEnter(PointerEventData eventData) {
		if (index == 1) {
			StartCoroutine(ScaleTo(btjogar, Vector3.one, 0.2f));

			btajustes.pivot = new Vector2(0, 1);
			StartCoroutine(ScaleTo(btajustes, new Vector3(0.5f, 0.5f, 1), 0.2f));
			StartCoroutine(ScaleTo(btsair, new Vector3(0.5f, 0.5f, 1), 0.2f));

			StartCoroutine(MoveTo(bsair, new Vector2(bsair.anchoredPosition.x, -97), 0.2f));
			StartCoroutine(MoveTo(bjogar, new Vector2(bjogar.anchoredPosition.x, 50), 0.2f));
		}

		if (index == 2) {
			StartCoroutine(ScaleTo(btajustes, Vector3.one, 0.2f));

			StartCoroutine(ScaleTo(btjogar, new Vector3(0.5f, 0.5f, 1), 0.2f));
			StartCoroutine(ScaleTo(btsair, new Vector3(0.5f, 0.5f, 1), 0.2f));

			StartCoroutine(MoveTo(bsair, new Vector2(bsair.anchoredPosition.x, -130), 0.2f));
			StartCoroutine(MoveTo(bjogar, new Vector2(bjogar.anchoredPosition.x, 50), 0.2f));
		}

		if (index == 3) {
			StartCoroutine(ScaleTo(btsair, Vector3.one, 0.2f));

			btajustes.pivot = new Vector2(0, 0);
			StartCoroutine(ScaleTo(btajustes, new Vector3(0.5f, 0.5f, 1), 0.2f));
			StartCoroutine(ScaleTo(btjogar, new Vector3(0.5f, 0.5f, 1), 0.2f));

			StartCoroutine(MoveTo(bsair, new Vector2(bsair.anchoredPosition.x, -130), 0.2f));
			StartCoroutine(MoveTo(bjogar, new Vector2(bjogar.anchoredPosition.x, 17), 0.2f));
		}
	}

	IEnumerator ScaleTo(RectTransform target, Vector3 targetScale, float duration) {
		Vector3 initialScale = target.localScale;
		float time = 0;

		while (time < duration) {
			target.localScale = Vector3.Lerp(initialScale, targetScale, time / duration);
			time += Time.deltaTime;
			yield return null;
		}

		target.localScale = targetScale;
	}

	IEnumerator MoveTo(RectTransform target, Vector2 targetPos, float duration) {
		Vector2 initialPos = target.anchoredPosition;
		float time = 0;

		while (time < duration) {
			target.anchoredPosition = Vector2.Lerp(initialPos, targetPos, time / duration);
			time += Time.deltaTime;
			yield return null;
		}

		target.anchoredPosition = targetPos;
	}
}