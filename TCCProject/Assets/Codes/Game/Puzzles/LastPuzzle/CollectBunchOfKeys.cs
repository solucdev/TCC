using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBunchOfKeys : MonoBehaviour
{
    [SerializeField] Inventory inv;
    [SerializeField] Transform target;
    bool moving;

	private void Update() {
		if (moving) {
			MoveRotate();
		}
	}
    public void StartMoveRotate() {
        transform.localScale = new Vector3(transform.localScale.x * 5, transform.localScale.y * 5, transform.localScale.z * 5);
        moving = true;
    }
    private void MoveRotate() {
        transform.parent = target.parent;
		transform.position = Vector3.MoveTowards(transform.position, target.position, 1.5f * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, 30 * Time.deltaTime);
        if(transform.position == target.position && transform.rotation == target.rotation)
        {
            moving = false;
        }
    }
}
