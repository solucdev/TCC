using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbToAttic : MonoBehaviour
{
	[SerializeField] GameObject player;
	Animator anim;
    void Start()
    {
		anim = player.GetComponent<Animator>();
		anim.enabled = false;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")){
			anim.enabled = true;
			anim.Play("climb_attic");
			//ENABLED = FALSE
		}
	}
}
