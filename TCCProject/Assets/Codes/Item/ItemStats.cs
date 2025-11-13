using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemStats : MonoBehaviour {
	[SerializeField] ItemObject scr;
	public ItemObject Scr => scr;
	public string itemname;
	public string itemID;
	public float scale;
	public Sprite icon;
	[SerializeField] Transform handfit;

	[SerializeField] Text i;
	public Text f;

	void Start() {
		itemname = scr.itemName;
		icon = scr.icon;
		scale = scr.scale;
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
	public void Feedback() {
		f.text = "Coletou " + itemname;
	}

}
