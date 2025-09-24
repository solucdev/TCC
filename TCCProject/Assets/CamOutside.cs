using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOutside : MonoBehaviour

{
    [SerializeField] float sensX;
    [SerializeField] float sensY;
    [SerializeField] SenseConfig sensebindx;
    [SerializeField] SenseConfig sensebindy;

    [SerializeField] Transform orientation; // rotação horizontal
    [SerializeField] Transform cameraRoot;  // rotação vertical

    public Transform player;

    private float xRotation;
    private float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UpdateSense();
    }

    void Update()
    {
        /*float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;*/

        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;


        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        player.rotation = orientation.rotation;

    }

    public void UpdateSense()
    {
        sensX = sensebindx.sense.value;
        sensY = sensebindy.sense.value;
    }
}
