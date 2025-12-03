using System.Collections;
using UnityEngine;

public class AmuletFinish : MonoBehaviour {
    [SerializeField] Transform amulet;
    [SerializeField] Transform targetSlot;
    [SerializeField] Transform keySpawn;
    [SerializeField] GameObject keyObject;
    [SerializeField] Camera cam;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float rotateSpeed = 90f;

    bool moving;
    bool finished;

    public void StartFinish() {
        if (!finished) {
            moving = true;
            StartCoroutine(FinishSequence());
        }
    }

    IEnumerator FinishSequence() {
        while (Vector3.Distance(amulet.position, targetSlot.position) > 0.01f) {
            amulet.position = Vector3.MoveTowards(amulet.position, targetSlot.position, moveSpeed * Time.deltaTime);
            amulet.rotation = Quaternion.RotateTowards(amulet.rotation, targetSlot.rotation, rotateSpeed * Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, amulet.position + new Vector3(0, 2, -4), 2f * Time.deltaTime);
            cam.transform.LookAt(amulet);
            yield return null;
        }
        amulet.position = targetSlot.position;
        amulet.rotation = targetSlot.rotation;
        yield return new WaitForSeconds(1f);
        keyObject.transform.position = keySpawn.position;
        keyObject.SetActive(true);
        finished = true;
    }
}