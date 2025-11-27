using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    private Light lighte;
    public GameObject flashlight;
    public GameObject txtC;
    public GameObject cam;
    [SerializeField] int range;
    [SerializeField] GameObject txtF;

    private void Start()
    {
        lighte = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            lighte.enabled = !lighte.enabled;
            flashlight.SetActive(lighte.enabled);
            txtF.SetActive(false);
        }

        RaycastHit hit;
        Vector3 direction = cam.transform.forward;

        if (Physics.Raycast(cam.transform.position, direction, out hit, range))
        {
            if (hit.collider.gameObject.name == "ligs scripts")
            {
                txtC.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.gameObject.SetActive(false);
                    flashlight.SetActive(true);
                    txtF.SetActive(true);
                }
            }
            else
            {
                txtC.SetActive(false);
            }
        }
    }
}
