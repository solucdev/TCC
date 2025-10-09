using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
	public List<GameObject> inventory = new List<GameObject>();
	public Transform camItems;
	[SerializeField] List<Image> slots = new List<Image>();

	ItemDrop drop;
	int lastItem = -1;
	GameObject useditem;
	int MaxSlots = 8;

	private void Start() {
		drop = GetComponent<ItemDrop>();
		useditem = null;
		ReorganizeIcons();
	}

	private void Update() {
		useditem = (lastItem >= 0 && lastItem < inventory.Count) ? inventory[lastItem] : null;

		if (Input.GetKeyDown(KeyCode.Q) && lastItem >= 0 && lastItem < inventory.Count) {
			GameObject item = inventory[lastItem];
			inventory.RemoveAt(lastItem);
			drop.Drop(item);

			lastItem = -1;
			useditem = null;
			ReorganizeIcons();
		}
	}

	public void AddItem(GameObject item) {
		if (inventory.Contains(item)) return;

		if (inventory.Count >= MaxSlots) {
			drop.Drop(item);
			return;
		}
		item.layer = LayerMask.NameToLayer("MyItem");
		item.SetActive(false);
		inventory.Add(item);
		ReorganizeIcons();
	}

	public void RemoveItem(GameObject item) {
		if (!inventory.Contains(item)) return;
		else { 
			inventory.Remove(item);
			Destroy(item);
			ReorganizeIcons();
		}
	}

	public void PlaceItem(GameObject item, Transform transform) 
    {
		if (!inventory.Contains(item)) return;
		else {
			inventory.Remove(item);
			item.transform.SetParent(null);
			item.SetActive(true);
			item.transform.position = transform.position;
			item.transform.rotation = transform.rotation;
			SetLayerAllChildrens(item, "Item"); 
			ReorganizeIcons();
		}
	}

	public void ActiveItem(int slot) {

		if (lastItem >= 0 && lastItem < inventory.Count) { //tirar o ultimo
			GameObject lastPrefab = inventory[lastItem];
			lastPrefab.SetActive(false);
			lastItem = -1;
		}

		if (slot < inventory.Count) {
			GameObject prefab = inventory[slot];
			ItemStats script = prefab.GetComponent<ItemStats>();

			script.PerfectPosition(camItems);
			prefab.SetActive(true);

			lastItem = slot;
		}
		else { return; }
	}

	public bool ItemInHand(GameObject item) {
		if (lastItem >= 0 && lastItem < inventory.Count) {
			GameObject activeItem = inventory[lastItem];
			return activeItem == item;
		}
		return false;
	}

	void ReorganizeIcons() {
		for (int i = 0; i < slots.Count; i++) {
			if (i < inventory.Count && inventory[i] != null) {

				ItemStats scripter = inventory[i].GetComponent<ItemStats>();
				slots[i].sprite = (scripter != null) ? scripter.icon : null;
				slots[i].color = new Color(slots[i].color.r, slots[i].color.g, slots[i].color.b, 1);

			} else {
				slots[i].sprite = null;
				slots[i].color = new Color(slots[i].color.r, slots[i].color.g, slots[i].color.b, 0);
			}
		}
	}

	private void SetLayerAllChildrens(GameObject obj, string layerName) {
		int layer = LayerMask.NameToLayer(layerName);
		foreach (Transform t in obj.GetComponentsInChildren<Transform>(true)) {
			t.gameObject.layer = layer;
		}
	}
}