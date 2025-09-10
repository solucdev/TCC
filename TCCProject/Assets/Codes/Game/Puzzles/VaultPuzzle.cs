using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class VaultPuzzle : MonoBehaviour
{
    [SerializeField] Inventory inv;
    [SerializeField] PlayerCrouchCam orientation;
	[SerializeField] PlayerCam playercam;
	[SerializeField] PlayerMove playermove;
	[SerializeField] CamBreath cambreath;
	[SerializeField] GameObject i;
    [SerializeField] GameObject amulet;
	[SerializeField] Transform cam;
	[SerializeField] Transform camholder;
    [SerializeField] Transform placer;
    [SerializeField] Transform target;
    public List<Transform> places = new List<Transform>();
    private List<GameObject> pieces = new List<GameObject>();

    Vector3 plc;
    int index = 0;
    bool intrigger;
    bool assembling;
    bool finish;
    
    void Start()
    {
        plc = places[0].position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && intrigger)
        {
            Debug.Log(index);
            if (index < 3)
            {
                SetPieces();
            }
            else
            { StartVaultPuzzle();}
        }

        if(Input.GetKeyDown(KeyCode.E)  && assembling) { cam.rotation = Quaternion.Euler(45, 0, 0); }

		if (assembling) {
			AssembleAmulet();
		}
        if (finish)
        {
            FinishVaultPuzzle();
        }
	}

	void SetPieces()
    {
        for (int i = 0; i < inv.inventory.Count; i++)
        {
            GameObject prefab = inv.inventory[i];
            ItemStats scripter = prefab.GetComponent<ItemStats>();

            if (scripter.itemname == "Peça de Amuleto")
            {
                GameObject clone = Instantiate(prefab, placer);
                clone.transform.position = plc;
                clone.transform.localScale = new Vector3(clone.transform.localScale.x * 20, clone.transform.localScale.y * 20, clone.transform.localScale.z * 20);
                clone.SetActive(true);
				clone.layer = LayerMask.NameToLayer("Default");
                pieces.Add(clone);
                inv.RemoveItem(prefab);

                index++;
                if (index < places.Count) {
                    plc = places[index].position;
                }
                break;
            }
        }
    }

    void StartVaultPuzzle()
    {
        orientation.enabled = false;
        playercam.enabled = false;
        playermove.enabled = false;
        cambreath.enabled = false;
		cam.rotation = Quaternion.Euler(0, 0, 0);
		camholder.position = new Vector3(154.65f, 5, 242);
		camholder.rotation = Quaternion.Euler(45, 0, 0);

        assembling = true;
    }

    void FinishVaultPuzzle()
    {
        orientation.enabled = true;
        playercam.enabled = true;
        playermove.enabled = true;
        cambreath.enabled = true;
        camholder.rotation = Quaternion.Euler(0, 0, 0);
    }

    async void AssembleAmulet()
    {
		Vector3 instapos = target.position;

		for (int i = 0; i < pieces.Count; i++)
        {
            GameObject amp = pieces[i];
            Transform ampt = amp.transform;
            ampt.position = Vector3.MoveTowards(ampt.position, target.position, 0.5f * Time.deltaTime);
            if(ampt.position == target.position) {
               await Task.Delay(400);
                Destroy(amp);
				pieces.Remove(amp);
				amulet.SetActive(true);
                break;
            }
        }

        if (amulet.activeSelf)
        {
            await Task.Delay(6000);
            amulet.SetActive(false);
            finish = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) { 

            if (orientation.enabled)
            {
                i.SetActive(true);
                intrigger = true;
            }
            else
            {
                i.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) {
            i.SetActive(false);
            intrigger = false;
        }
    }
}
