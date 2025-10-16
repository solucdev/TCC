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
    }
    public IEnumerator DelayCollect(Disable disable) {
        yield return new WaitForSeconds(3);
          gameObject.layer = LayerMask.NameToLayer("Item");
        if (gameObject.layer == LayerMask.NameToLayer("MyItem")) {
            disable.EnablePlayer();
        }
	}
}
