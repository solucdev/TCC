using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour {
	[SerializeField] float angle = 90;
	private int speed = 2;
	public bool locked;
	public GameObject key;

    bool opened = false;
	Quaternion closedRotation;
	Quaternion openRotation;
    public string roomName;

	void Start() {
		closedRotation = transform.rotation;
		openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, angle, 0));
	}

	void Update() {
	}

	public IEnumerator ToggleDoor() {

        if (locked && key != null)
        {
            locked = false;
        }

        if (locked)
        {
            yield break;
        }

        Quaternion targetRotation;
        Quaternion startRotation = transform.rotation;

        if (opened)
        {
            targetRotation = closedRotation;
        }
        else
        {
            targetRotation = openRotation;
        }
        opened = !opened;

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
    public bool IsOpen => opened;
}
