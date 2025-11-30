using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QTEPure : MonoBehaviour {
	[SerializeField] GameObject qteUI;
	public List<KeyCode> KeyButtons = new List<KeyCode>();
	public List<Sprite> KeySprites = new List<Sprite>();
	[SerializeField] Image buttonPlace;
	[SerializeField] Image errorflash;

	public float timer;
	private float startTime;
	private float elapsed;
	private KeyCode currentKey;
	[HideInInspector] public bool onQTE = false;

	public Disable disable;
	private bool arrest;

	private int totalRounds;
	private int currentRound;

	// Flag pública para o NavMeshMove saber o resultado
	public bool PlayerSurvived { get; private set; }

	void Start() {
		startTime = timer;
	}

	void Update() {
		if (!onQTE) return;

		elapsed += Time.deltaTime;

		// Acertou a tecla correta
		if (Input.GetKeyDown(currentKey) && elapsed < timer) {
			currentRound++;
			if (currentRound >= totalRounds) {
				disable.EnablePlayer();
				qteUI.SetActive(false);
				onQTE = false;
				PlayerSurvived = true;   // sucesso
				Debug.Log("QTE completo!");
			} else {
				elapsed = 0;
				timer = startTime;
				ShowNewLetter();
			}
			return;
		}

		// Tempo acabou
		if (elapsed > timer) {
			qteUI.SetActive(false);
			onQTE = false;
			arrest = true;
			ArrestPlayer();
			PlayerSurvived = false;     // falhou
			Debug.Log("QTE falhou!");
		}

		// Penalidade em tecla errada
		if (Input.anyKeyDown) {
			if (!Input.GetKeyDown(currentKey) && !IsAllowedKey()) {
				PiFlash();
				timer -= 1f;
			}
		}
	}

	public void StartQTE(float duration, int rounds) {
		onQTE = true;
		elapsed = 0;
		timer = duration;
		startTime = duration;

		totalRounds = rounds;
		currentRound = 0;

		PlayerSurvived = false; // reset resultado
		ShowNewLetter();
		qteUI.SetActive(true);
	}

	void ShowNewLetter() {
		int button = Random.Range(0, KeyButtons.Count);
		currentKey = KeyButtons[button];
		buttonPlace.sprite = KeySprites[button];

		FindObjectOfType<TimeBar>().ResetRound();
	}

	public void PiFlash() {
		errorflash.color = new Color(1, 0, 0, 0.5f);
		Invoke(nameof(ClearPF), 0.1f);
	}

	void ClearPF() {
		errorflash.color = new Color(1, 1, 1, 1f);
	}

	void ArrestPlayer() {
		disable.DisablePlayer();
	}

	public void ResetArrest() {
		arrest = false;
	}

	bool IsAllowedKey() {
		return Input.GetKeyDown(KeyCode.W) ||
			   Input.GetKeyDown(KeyCode.A) ||
			   Input.GetKeyDown(KeyCode.S) ||
			   Input.GetKeyDown(KeyCode.D) ||
			   Input.GetKeyDown(KeyCode.E) ||
			   Input.GetKeyDown(KeyCode.F) ||
			   Input.GetKeyDown(KeyCode.LeftShift) ||
			   Input.GetKeyDown(KeyCode.LeftControl) ||
			   Input.GetMouseButtonDown(0);
	}
}