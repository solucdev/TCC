using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StunEffect : MonoBehaviour
{
    private QTE qte;
    private TimeBar timebar;
    private NavMeshMove navmesh;
    private NavMeshAgent ai;
    public GameObject fbx;

    private void Start()
    {
        qte = GetComponent<QTE>();
        timebar = GetComponent<TimeBar>();
        navmesh = GetComponent<NavMeshMove>();
        ai = GetComponent<NavMeshAgent>();
    }
    public void DisableEnemy()
    {
        qte.enabled = false;
        timebar.enabled = false;
        navmesh.enabled = false;
        ai.enabled = false;
        fbx.GetComponent<Animator>().Play("walking to die");
    }
    public void ResetEnemy()
    {

    }
}
