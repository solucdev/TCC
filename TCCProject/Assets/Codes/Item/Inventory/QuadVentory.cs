using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuadVentory : MonoBehaviour
{
    [SerializeField] Inventory inv;
    [SerializeField] GameObject invset;
    [SerializeField] Transform slctdbox;
    [SerializeField] Transform cam;
    private Quaternion camb;
    [HideInInspector] public bool open = false;
    void Start()
    {
        camb = Quaternion.Euler(0, 0, 0);
	}
    void Update()
    {
        if (UIManager.Instance != null && UIManager.Instance.IsAnyUIOpen && !UIManager.Instance.isInventoryOpen)
            return;

        if (Input.GetKeyDown(KeyCode.I) && !open)
        {
            camb = cam.rotation;
            invset.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            StartCoroutine(delay(true));
            UIManager.Instance.isInventoryOpen = true;
        }
        if (Input.GetKeyDown(KeyCode.I) && open)
        {
            cam.rotation = camb;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
			Time.timeScale = 1;
			StartCoroutine(delay(false));
            invset.SetActive(false);
            UIManager.Instance.isInventoryOpen = false;
        }

    }
    public void Select(int slot)
    {
		cam.rotation = camb;
		inv.ActiveItem(slot);
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
		StartCoroutine(delay(false));
        invset.SetActive(false);
        UIManager.Instance.isInventoryOpen = false;
    }

    public void PlaceBox(Transform slotpos)
    {
        slctdbox.position = new Vector3(slotpos.position.x, slotpos.position.y - 40, slotpos.position.z);
    }
    IEnumerator delay(bool torf)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        open = torf;
    }
}
