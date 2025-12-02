using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEPure : MonoBehaviour {
	[SerializeField] GameObject qteUI;
	public List<KeyCode> KeyButtons = new List<KeyCode>();
	public List<Sprite> KeySprites = new List<Sprite>();
	[SerializeField] Image buttonPlace;
	[SerializeField] Image errorflash;

	[SerializeField] TimeBar timeBar; // injete via Inspector

	public float timer;         // duração atual da rodada
	private float startTime;    // duração base da rodada
	public float elapsed;       // tempo decorrido da rodada atual

	private KeyCode currentKey;
	[HideInInspector] public bool onQTE = false;

	public Disable disable;
	private bool arrest;

	private int totalRounds;
	private int currentRound;

	public bool PlayerSurvived { get; private set; }

	void Start() {
		startTime = timer;
	}

	void Update() {
		if (!onQTE) return;

		// Se você usa timescale em gameplay, troque por Time.unscaledDeltaTime
		elapsed += Time.deltaTime;

		bool correctPressed = Input.GetKeyDown(currentKey);
		bool anyPressed = Input.anyKeyDown;
		bool wrongPressed = anyPressed && !correctPressed && !IsAllowedKey();

		// 1) Acerto antes do tempo
		if (correctPressed && elapsed < timer) {
			currentRound++;
			if (currentRound >= totalRounds) {
				disable.EnablePlayer();
				qteUI.SetActive(false);
				onQTE = false;
				PlayerSurvived = true;
				Debug.Log("QTE completo!");
			} else {
				// Próxima rodada
				elapsed = 0f;
				timer = startTime;
				ShowNewLetter();
				timeBar.ResetRound(); // apenas visual reset
			}
			return; // evita penalidade no mesmo frame
		}

		// 2) Penalidade por erro (não aplica se acertou no frame)
		if (wrongPressed) {
			PiFlash();
			// Reduzir timer é perigoso; prefira aumentar elapsed para penalizar sem alterar duração alvo
			// Exemplo alternativo:
			elapsed += 1f; // penalidade: avança o tempo em 1 segundo
						   // Se quiser manter a sua lógica original, saiba que a barra lerpa com timer novo e pode desalinhar
						   // timer = Mathf.Max(0.5f, timer - 1f); // clamp mínimo
		}

		// 3) Tempo acabou
		if (elapsed >= timer) {
			qteUI.SetActive(false);
			onQTE = false;
			arrest = true;
			ArrestPlayer();
			PlayerSurvived = false;
			Debug.Log("QTE falhou!");
		}
	}

	public void StartQTE(float duration, int rounds) {
		onQTE = true;
		elapsed = 0f;
		timer = duration;
		startTime = duration;

		totalRounds = rounds;
		currentRound = 0;

		PlayerSurvived = false;
		ShowNewLetter();
		qteUI.SetActive(true);

		// Informe a barra para sincronizar com a rodada atual
		if (timeBar != null) timeBar.Begin(enemie: this);
	}

	void ShowNewLetter() {
		int button = Random.Range(0, KeyButtons.Count);
		currentKey = KeyButtons[button];
		buttonPlace.sprite = KeySprites[button];
	}

	public void PiFlash() {
		errorflash.color = new Color(1, 0, 0, 0.5f);
		CancelInvoke(nameof(ClearPF));
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