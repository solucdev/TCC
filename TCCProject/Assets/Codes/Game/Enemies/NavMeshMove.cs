using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavMeshMove : MonoBehaviour {
	NavMeshAgent ai;
	public List<Transform> points = new List<Transform>();
	[SerializeField] int idleTime;
	[SerializeField] int difficult;
	[SerializeField] Transform player;
	[SerializeField] GameObject fbx;

	int randpoint;
	bool fp;
	Vector3 destination;

	void Start() {
		ai = GetComponent<NavMeshAgent>();
		defaultFOV = playerCam.fieldOfView;
		Invoke(nameof(AIWalk), 2f);
	}

	private void Update() {
		if (!ai.pathPending && ai.remainingDistance <= ai.stoppingDistance && !fp) {
			fbx.GetComponent<Animator>().Play("idle");
			StartCoroutine(IdleDelay());
		}

		if (ai.speed < 0.2f) {
			fbx.GetComponent<Animator>().Play("idle");
		}

		Debug.DrawRay(fbx.transform.position, Vector3.up * safeDistance, Color.red);
		Debug.DrawRay(fbx.transform.position, Vector3.forward * safeDistance, Color.red);
		Debug.DrawRay(fbx.transform.position, Vector3.right * safeDistance, Color.red);
	}

	IEnumerator IdleDelay() {
		yield return new WaitForSeconds(idleTime);
		AIWalk();
	}

	void RandomPoint() {
		randpoint = Random.Range(0, points.Count);
		destination = points[randpoint].position;
	}

	void AIWalk() {
		fbx.GetComponent<Animator>().Play("swagger");
		if (!fp) {
			RandomPoint();
			ai.SetDestination(destination);
		}
	}

	[SerializeField] Transform head;
	[SerializeField] Camera playerCam;
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip firstSound;
	[SerializeField] AudioClip secondSound;
	[SerializeField] List<Light> lights = new List<Light>();
	[SerializeField] Disable system;

	[SerializeField] List<ScanPlayer> scanners = new List<ScanPlayer>();
	[SerializeField] QTEPure qte;

	[SerializeField] GameObject indicator;
	[SerializeField] AudioSource backgroundAudio;
	private float defaultFOV;

	private bool encontroAtivo = false;

	
	[SerializeField] PlayerCrouchCam crouchCam;
	[SerializeField] float safeDistance = 10f;

	public void TriggerEncounter() {
		if (encontroAtivo) return; // só uma vez por encontro
		encontroAtivo = true;
		fp = true;

		ai.isStopped = true;
		ai.ResetPath();
		fbx.GetComponent<Animator>().Play("idle");

		if (backgroundAudio != null && backgroundAudio.isPlaying) {
			backgroundAudio.Pause();
		}

		system.DisablePlayer();
		StartCoroutine(FaceEnemySequence());
	}

	IEnumerator FaceEnemySequence() {
		DisableAllScanners();

		FaceEnemy();
		StartCoroutine(ZoomIn());
		StartCoroutine(CameraShake(0.3f, 1f));
		yield return new WaitForSeconds(2f);
		EndEncounter();
	}

	void EndEncounter() {
		indicator.SetActive(true);
		audioSource.Stop();
		system.EnablePlayer();

		foreach (Light l in lights) {
			l.enabled = false;
		}

		gameObject.name = "INIMIGO DESATIVADO";
		fbx.SetActive(false);
		playerCam.fieldOfView = defaultFOV;

		audioSource.clip = secondSound;
		audioSource.Play();

		// inicia QTE e espera resultado
		qte.StartQTE(6, 3);
		StartCoroutine(CameraShake(0.02f, secondSound.length));

		// escuta resultado do QTE
		StartCoroutine(WaitQTEResult());
	}

	IEnumerator WaitQTEResult() {
		while (qte.onQTE) {
			yield return null;
		}

		if (!qte.PlayerSurvived) {
			indicator.SetActive(false);
			PlayerDeathManager pdm = FindObjectOfType<PlayerDeathManager>();
			if (pdm != null) pdm.PlayerDied();
			foreach (Light l in lights) { l.enabled = true; }
			StartCoroutine(ReappearEnemy());
		} else {
			

			// pega tempo de sobra do QTE
			float sobraTempo = Mathf.Max(0f, qte.timer - qte.elapsed);

			// dá ao player esse tempo extra para fugir
			yield return new WaitForSeconds(sobraTempo);
			indicator.SetActive(false);
			float dist = Vector3.Distance(player.position, fbx.transform.position);

			if (dist < safeDistance || !crouchCam.isdown) {
				PlayerDeathManager pdm = FindObjectOfType<PlayerDeathManager>();
				if (pdm != null) pdm.PlayerDied();
				foreach (Light l in lights) { l.enabled = true; }
				StartCoroutine(ReappearEnemy());
			} else {
				foreach (Light l in lights) { l.enabled = true; }
				if (backgroundAudio != null) backgroundAudio.UnPause();
				StartCoroutine(ReappearEnemy());
			}
		}
	}

	IEnumerator ReappearEnemy() {
		yield return new WaitForSeconds(60f);

		gameObject.name = "INIMIGO ATIVO";
		fbx.SetActive(true);
		EnableAllScanners();
		fp = false;
		AIWalk();
		encontroAtivo = false;
	}

	IEnumerator CameraShake(float intensity, float duration) {
		Vector3 originalPos = playerCam.transform.localPosition;
		float elapsed = 0f;

		while (elapsed < duration) {
			float x = Random.Range(-1f, 1f) * intensity;
			float y = Random.Range(-1f, 1f) * intensity;
			playerCam.transform.localPosition = originalPos + new Vector3(x, y, 0);

			elapsed += Time.deltaTime;
			yield return null;
		}

		playerCam.transform.localPosition = originalPos;
	}

	IEnumerator ZoomIn() {
		float startFOV = playerCam.fieldOfView;
		float targetFOV = 30f;
		float duration = 0.3f;
		float elapsed = 0f;

		while (elapsed < duration) {
			playerCam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsed / duration);
			elapsed += Time.deltaTime;
			yield return null;
		}

		playerCam.fieldOfView = targetFOV;
	}

	void FaceEnemy() {
		Vector3 dir = head.position - playerCam.transform.position;
		Quaternion lookRot = Quaternion.LookRotation(dir);

		playerCam.transform.rotation = lookRot;
		player.rotation = lookRot;

		audioSource.clip = firstSound;
		audioSource.Play();
	}

	void DisableAllScanners() {
		foreach (ScanPlayer sp in scanners) {
			if (sp != null) sp.enabled = false;
		}
	}

	void EnableAllScanners() {
		foreach (ScanPlayer sp in scanners) {
			if (sp != null) sp.enabled = true;
		}
	}
}