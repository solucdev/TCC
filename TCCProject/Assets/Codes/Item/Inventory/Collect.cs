using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour {
	[SerializeField] LayerMask collects;
	[SerializeField] GameObject i;
	[SerializeField] Transform handfit;
	[SerializeField] float range;
	[SerializeField] Inventory inv;
	[SerializeField] GameObject Keys;

	void Update() {
		RaycastHit hit;
		Vector3 direction = transform.forward;

		Debug.DrawRay(transform.position, direction * range);

		if (Physics.Raycast(transform.position, direction, out hit, range, collects)) {

			GameObject itemPrefab = hit.collider.gameObject;
			ItemStats script = itemPrefab.GetComponent<ItemStats>();

			if (script != null) {
				i.SetActive(true);
				script.ShowName();

				if (Input.GetMouseButtonDown(0)) {
					script.Feedback();
					StartCoroutine(ClearText(script));

					SetLayerAllChildrens(itemPrefab, "MyItem");
					//itemPrefab.layer = LayerMask.NameToLayer("MyItem");
					inv.AddItem(itemPrefab);

					if (script.Scr.itemID.StartsWith("amuleto_"))
					{
						ItemCollectionTracker.Instance.RegisterAmuletoPiece(script.Scr.itemID);
					}

					if (!ItemCollectionTracker.Instance.HasCollected(script.Scr.itemName))
					{
						ItemCollectionTracker.Instance.RegisterItem(script.Scr.itemName);

						// Mensagens específicas por item
						switch (script.Scr.itemID)
						{

                            case "Banquinho":
								Objetivos.Instance.SetObjective("Coloque o banquinho no lugar certo");
								break;
							case "Pé de Cabra":
								Objetivos.Instance.SetObjective("Use o pé de cabra no alçapão para fugir do sótão");
								break;

							/*default:
								Objetivos.Instance.SetObjective($"Você coletou: {script.itemname}");
								break;*/
							case "Chave da Biblioteca":
								Objetivos.Instance.SetObjective("Abra a biblioteca");
								break;
							case "Chave da Seita":
								Objetivos.Instance.SetObjective("Abra o acervo da seita");
								break;
							case "chave do escritório":
								Objetivos.Instance.SetObjective("Abra o escritório");
								break;
							case "Molho de Chaves":
								Objetivos.Instance.SetObjective("Abra a porta principal e fuja da casa");
								Keys.GetComponent<QTEKeys>().enabled = false;
                                Keys.GetComponent<TimeBarKeys>().enabled = false;
                                break;
						
						}
					}

					itemPrefab.transform.localScale = new Vector3(itemPrefab.transform.localScale.x * script.scale,
					itemPrefab.transform.localScale.y * script.scale, itemPrefab.transform.localScale.z * script.scale);
					itemPrefab.SetActive(false);
				}
			}
		}
		else
		{
			i.SetActive(false);
		}
	}

	IEnumerator ClearText(ItemStats scr) {
		yield return new WaitForSeconds(2);
		scr.f.text = "";
	}

	private void SetLayerAllChildrens(GameObject obj, string layerName) {
		int layer = LayerMask.NameToLayer(layerName);
		foreach (Transform t in obj.GetComponentsInChildren<Transform>(true)) {
			t.gameObject.layer = layer;
		}
	}


}
