using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    [SerializeField] float sensX;
    [SerializeField] float sensY;
    [SerializeField] SenseConfig sensebindx;
	[SerializeField] SenseConfig sensebindy;

	[SerializeField] Transform player;

    private float xRotation;
    private float yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;

        yRotation = yRotation + mouseX;
        xRotation = xRotation - mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            player.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void UpdateSense() {
        sensX = sensebindx.sense.value;
        sensY = sensebindy.sense.value;
    }
}