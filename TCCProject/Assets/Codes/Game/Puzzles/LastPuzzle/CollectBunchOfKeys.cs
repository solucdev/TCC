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
        moving = true;
    }
    private void MoveRotate() {
		transform.position = Vector3.MoveTowards(transform.position, target.position, 1.5f * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, 30 * Time.deltaTime);
        if(transform.position == target.position)
        {
            moving = false;
        }
    }
}
