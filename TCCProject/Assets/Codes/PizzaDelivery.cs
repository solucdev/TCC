using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaDelivery : MonoBehaviour {
	public GameObject pizza;

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) { 
			pizza.SetActive(true);
		}
	}
}

