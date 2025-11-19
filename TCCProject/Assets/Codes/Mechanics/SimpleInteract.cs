using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteract : MonoBehaviour
{
    [SerializeField] float angle;
    private int speed = 2;
    bool opened;
    Quaternion closedRotation;
    Quaternion openRotation;

    void Update()
    {
        RaycastHit hit;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(transform.position, direction, out hit, 3))
        {
            GameObject door = hit.collider.gameObject;
            GameObject mind = door.GetComponentInParent<GameObject>();

            StartCoroutine(ToggleDoor(mind));
            closedRotation = door.transform.rotation;
            openRotation = Quaternion.Euler(door.transform.eulerAngles + new Vector3(0, angle, 0));
        }
    }

        public IEnumerator ToggleDoor(GameObject door)
    { 
        Quaternion targetRotation;
        Quaternion startRotation = door.transform.rotation;

        if (opened)
        {targetRotation = closedRotation;}
        else
        {targetRotation = openRotation;}
        opened = !opened;

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * speed;
            door.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed);
            yield return null;
        }

        door.transform.rotation = targetRotation;
    }

}
