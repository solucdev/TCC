using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class QTE : MonoBehaviour {
    [SerializeField] GameObject qte;

    public List<KeyCode> KeyButtons = new List<KeyCode>();
    public List<Sprite> KeySprites = new List<Sprite>();
	[SerializeField] Image buttonPlace;
    [SerializeField] Image errorflash;
    public float timer;
    private float startime;
    private float tsecs;
    private KeyCode stringkey;
    [HideInInspector] public bool onqte = false;

	private void Start() {
        startime = timer;
	}
	private void Update()
    {
        tsecs += Time.deltaTime;

        if(Input.GetKeyDown(stringkey) && tsecs < timer && onqte)
        {
            Debug.Log("legal");
            qte.SetActive(false);
            onqte = false;
        }
        if(tsecs > timer && onqte) {
            Debug.Log("perdeu o qte");
            timer = startime;
			qte.SetActive(false);
			onqte = false;
		}
        if (Input.anyKeyDown  && !Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A)
			 && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D) && !Input.GetKeyDown(KeyCode.LeftShift)
			  && !Input.GetKeyDown(KeyCode.LeftControl) && !Input.GetKeyDown(stringkey) && tsecs < timer && onqte) {
            PiFlash();
            timer -= 1f;
		}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tsecs > timer)
        {
            qte.SetActive(true);
            OnQTE();
        }
    }

    void OnQTE()
    {
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
		errorflash.color = new Color(1, 0, 0, 0f);
	}

}
