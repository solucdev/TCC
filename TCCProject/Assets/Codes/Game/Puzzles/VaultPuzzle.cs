using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultPuzzle : MonoBehaviour {
	public List<Transform> places = new List<Transform>();
	[SerializeField] private Inventory inv;
	[SerializeField] private PlayerCrouchCam orientation;
	[SerializeField] private PlayerCam playercam;
	[SerializeField] private PlayerMove playermove;
	[SerializeField] private CamBreath cambreath;
	[SerializeField] private GameObject i;
	[SerializeField] private GameObject amulet;
	[SerializeField] private Transform cam;
	[SerializeField] private Transform camholder;
	[SerializeField] private Transform placer;
	[SerializeField] private Transform target;
	[SerializeField] private GameObject keyObject;
	[SerializeField] private Collider puzzleCollider;
	[SerializeField] private AmuletFinish amuletFinish;

	private List<GameObject> pieces = new List<GameObject>();
	private Vector3 plc;
	private int index = 0;
	private bool intrigger;
	private bool assembling;
	private bool finish;

	void Start() {
		plc = places[0].position;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.E) && intrigger) {
			if (index < 3)
				SetPieces();
			else
				StartVaultPuzzle();
		}
		if (assembling) AssembleAmulet();
		if (finish) FinishVaultPuzzle();
	}

	private void SetPieces() {
		for (int i = 0; i < inv.inventory.Count; i++) {
			GameObject prefab = inv.inventory[i];
			if (ItemIsAmulet(prefab)) {
				CreatePiece(prefab);
				AddIndex();
				break;
			}
		}
	}

	private bool ItemIsAmulet(GameObject prefab) {
		return GetItemStats(prefab).itemname == "Peça de Amuleto";
	}

	private void CreatePiece(GameObject prefab) {
		GameObject piece = Instantiate(prefab, placer);
		PlacePiece(piece);
		pieces.Add(piece);
		inv.RemoveItem(prefab);
	}

	private void PlacePiece(GameObject piece) {
		piece.transform.position = plc;
		piece.transform.localScale = new Vector3(piece.transform.localScale.x / 4, piece.transform.localScale.y / 4, piece.transform.localScale.z / 4);
		piece.SetActive(true);
		piece.layer = LayerMask.NameToLayer("Default");
	}

	private void AddIndex() {
		index++;
		if (index < places.Count) {
			plc = places[index].position;
		}
	}

	private ItemStats GetItemStats(GameObject obj) {
		return obj.GetComponent<ItemStats>();
	}

	private void StartVaultPuzzle() {
		orientation.enabled = false;
		playercam.enabled = false;
		playermove.enabled = false;
		cambreath.enabled = false;
		cam.rotation = Quaternion.Euler(0, 0, 0);
		camholder.position = new Vector3(154.65f, 5, 242);
		camholder.rotation = Quaternion.Euler(45, 0, 0);
		assembling = true;
	}

	private bool puzzleCompleted = false;

	private void FinishVaultPuzzle() {
		if (puzzleCompleted) return;
		puzzleCompleted = true;
		orientation.enabled = true;
		playercam.enabled = true;
		playermove.enabled = true;
		cambreath.enabled = true;
		camholder.rotation = Quaternion.Euler(0, 0, 0);
		puzzleCollider.enabled = false;
		this.enabled = false;
		amuletFinish.StartFinish();
	}

	private async void AssembleAmulet() {
		for (int i = 0; i < pieces.Count; i++) {
			GameObject amp = pieces[i];
			Transform ampt = amp.transform;
			ampt.position = Vector3.MoveTowards(ampt.position, target.position, 0.5f * Time.deltaTime);
			if (ampt.position == target.position) {
				await System.Threading.Tasks.Task.Delay(400);
				Destroy(amp);
				pieces.Remove(amp);
				amulet.SetActive(true);
				break;
			}
		}
		if (amulet.activeSelf) {
			await System.Threading.Tasks.Task.Delay(2000);
			finish = true;
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.CompareTag("Player")) {
			if (orientation.enabled) {
				i.SetActive(true);
				intrigger = true;
			} else {
				i.SetActive(false);
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			i.SetActive(false);
			intrigger = false;
		}
	}
}