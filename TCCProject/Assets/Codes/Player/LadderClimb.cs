using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    [SerializeField] Disable able;
    [SerializeField] Transform player;
    bool yea;
    void Start()
    {
        
    }

    void Update()
    {
        if (yea && Input.GetKey(KeyCode.S))
        {
            player.position += new Vector3(0, -1, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.rotation = Quaternion.Euler(0, -90, 0);
            able.DisablePlayer();
            yea = true;
        }
    }
}
