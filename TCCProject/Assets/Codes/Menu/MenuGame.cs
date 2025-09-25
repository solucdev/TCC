using UnityEngine;
using UnityEngine.EventSystems;

public class MenuGame : MonoBehaviour, IPointerEnterHandler {
	[SerializeField] RectTransform btjogar;
	[SerializeField] RectTransform btajustes;
	[SerializeField] RectTransform btsair;

	[SerializeField] RectTransform bjogar;
	[SerializeField] RectTransform bsair;
	[SerializeField] int index;

	public void OnPointerEnter(PointerEventData eventData) {

		if (index == 1) {
			btjogar.localScale = new Vector3(1, 1);

			btajustes.pivot = new Vector2(0, 1);
			btajustes.localScale = new Vector3(0.5f, 0.5f);
			btsair.localScale = new Vector3(0.5f, 0.5f);
			bsair.anchoredPosition = new Vector3(bsair.anchoredPosition.x, -97);
			bjogar.anchoredPosition = new Vector3(bjogar.anchoredPosition.x, 50);

		}
		if (index == 2) {
			btajustes.localScale = new Vector3(1, 1);

			btjogar.localScale = new Vector3(0.5f, 0.5f);
			btsair.localScale = new Vector3(0.5f, 0.5f);
			bsair.anchoredPosition = new Vector3(bsair.anchoredPosition.x, -130);
			bjogar.anchoredPosition = new Vector3(bjogar.anchoredPosition.x, 50);

		}
		if (index == 3) {
			btsair.localScale = new Vector3(1, 1);

			btajustes.pivot = new Vector2(0, 0);
			btajustes.localScale = new Vector3(0.5f, 0.5f);
			btjogar.localScale = new Vector3(0.5f, 0.5f);
			bsair.anchoredPosition = new Vector3(bsair.anchoredPosition.x, -130);
			bjogar.anchoredPosition = new Vector3(bjogar.anchoredPosition.x, 17);
		}
	}
}