using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private Inventory inv;
    [SerializeField] private GameObject i;
    [SerializeField] private Text uiText;

    void Start()
    {
        
    }


    void Update() {
        RaycastHit hit;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(transform.position, direction, out hit, range)) {
            GameObject door = hit.collider.gameObject;
            Door script = door.GetComponentInParent<Door>();

            if (script != null) {


                string actionText = script.IsOpen ? "Fechar porta d" : "Abrir porta d";
                uiText.text = $"{actionText}{script.roomName}";
                uiText.enabled = true;


                if (Input.GetKeyDown(KeyCode.E)) {

                    if (!script.locked) {
                        StartCoroutine(script.ToggleDoor());
                    } 
                    else if (inv.ItemInHand(script.key)) {
						StartCoroutine(script.ToggleDoor());
					}
                    else { i.SetActive(true); StartCoroutine(Delay()); }

				}
            }
            
            else
        {
            uiText.enabled = false;
        }

        }
        else
        {
            uiText.enabled = false;
        }
    }
    IEnumerator Delay() {
        yield return new WaitForSeconds(2);
		i.SetActive(false);
	}
    }
