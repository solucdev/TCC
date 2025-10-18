using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class QTEKeys : MonoBehaviour {
	[SerializeField] GameObject qte;

	public List<KeyCode> KeyButtons = new List<KeyCode>();
	public List<Sprite> KeySprites = new List<Sprite>();
	[SerializeField] Image buttonPlace;
	[SerializeField] Image errorflash;
	[SerializeField] Transform head;
	CollectBunchOfKeys cbok;

	public int round = 69;
	public float timer;
	private float startime;
	private float tsecs;
	private KeyCode stringkey;
	[HideInInspector] public bool onqte = false;

	private void Start() {
		startime = timer;
		cbok = GetComponent<CollectBunchOfKeys>();
	}
	private void Update() {
		tsecs += Time.deltaTime;

		if (arrest) {
			ArrestPlayer();
		}

		if (Input.GetKeyDown(stringkey) && tsecs < timer && onqte) {
			if(round <= 7) {
				StartCoroutine(Delay());
			}
			else {
				cbok.StartMoveRotate();
				gameObject.layer = LayerMask.NameToLayer("Item");
				arrest = false;
				qte.SetActive(false);
				onqte = false;
                //StartCoroutine(EnableDelay());
                disable.EnablePlayer();
            }
        }
		if (tsecs > timer && onqte) {
			timer = startime;
			arrest = false;
			qte.SetActive(false);
			onqte = false;
			disable.EnablePlayer();
			round = 69;
		}
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A)
			 && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.LeftShift)
			  && !Input.GetKeyDown(KeyCode.LeftControl) && !Input.GetKeyDown(stringkey) && tsecs < timer && onqte) {
			PiFlash();
			timer -= 1f;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player") && tsecs > timer && gameObject.layer != LayerMask.NameToLayer("Item")) {
			round = 1;
			qte.SetActive(true);
			OnQTE();
			arrest = true;
		}
	}

	void OnQTE() {
		onqte = true;
		tsecs = 0;

		int button = Random.Range(0, KeyButtons.Count);
		stringkey = KeyButtons[button];
		buttonPlace.sprite = KeySprites[button];
	}
	public void PiFlash() {
		errorflash.color = new Color(1, 0, 0, 0.5f);
		Invoke(nameof(ClearPF), 0.1f);
	}

	void ClearPF() {
		errorflash.color = new Color(1, 1, 1, 1f);
	}

	[SerializeField] Transform cam;
	[SerializeField] Transform player;
	public Disable disable;
	private bool arrest;

	void ArrestPlayer() {
		disable.DisablePlayer();
		cam.rotation = Quaternion.LookRotation(head.position - cam.position);
	}

	public void ResetArrest() {
		arrest = false;
	}

	IEnumerator Delay() { 
		yield return new WaitForSeconds(0.2f);
		OnQTE();
		round++;
	}

	/*IEnumerator EnableDelay()
    {
		yield return new WaitForSeconds(2);
		disable.EnablePlayer();
	}*/
}
