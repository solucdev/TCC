using UnityEngine;
using UnityEngine.UI;

public class ItemStats : MonoBehaviour {
	[SerializeField] ItemObject scr;
	public string itemname;
	public Sprite icon;
	[SerializeField] Transform handfit;

	[SerializeField] Text i;

	void Start() {
		itemname = scr.itemName;
		icon = scr.icon;
	}

	public Transform PerfectPosition(Transform playerP) {
		transform.SetParent(playerP.transform, true);
		transform.position = handfit.position;
		transform.rotation = handfit.rotation;
		return transform;
	}

	public void ShowName()
	{
		i.text = "pressione 'M1' para coletar " + itemname;
	}
}
