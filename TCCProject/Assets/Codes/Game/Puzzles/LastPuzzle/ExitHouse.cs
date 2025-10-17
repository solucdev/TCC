using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHouse : MonoBehaviour
{
    [SerializeField] Inventory inv;
    [SerializeField] GameObject keys;
    [SerializeField] GameObject i;
    void Start()
    {

    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (inv.ItemInHand(keys))
            {
                StartExitPuzzle();
            }
            else
            {
                i.SetActive(true);
                StartCoroutine(Delay());
            }
        }
    }

    void StartExitPuzzle()
    {

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        i.SetActive(false);
    }
}

