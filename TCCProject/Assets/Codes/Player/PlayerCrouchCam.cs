using UnityEngine;

public class PlayerCrouchCam : MonoBehaviour {
    public Transform follower;
    [SerializeField] float h;
    [SerializeField] float speedc;
    public bool isdown;
    private Vector3 empe;

	private void Start() {
        empe = transform.localPosition;
    }

    private void FixedUpdate() {
        follower.position = transform.position; 
        Crouching();
    }
    public void Crouching() {
        if (Input.GetKey(KeyCode.LeftControl)) {
            isdown = true;
            transform.localPosition = Vector3.MoveTowards
           (transform.localPosition, empe + Vector3.down * h, Time.deltaTime * speedc);
        } else {
            isdown = false;
            transform.localPosition = Vector3.MoveTowards
           (transform.localPosition, empe, Time.deltaTime * speedc);
        }
    }
}