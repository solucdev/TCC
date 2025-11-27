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
    public bool stunned;

    private void Start()
    {
        qte = GetComponent<QTE>();
        timebar = GetComponent<TimeBar>();
        navmesh = GetComponent<NavMeshMove>();
        ai = GetComponent<NavMeshAgent>();
    }
    public void DisableEnemy()
    {
        GetComponent<Collider>().enabled = false;
        qte.enabled = false;
        timebar.enabled = false;
        navmesh.enabled = false;
        ai.enabled = false;
        stunned = true;
        fbx.GetComponent<Animator>().Play("walking to die");
        StartCoroutine(ResetEnemy());
    }
    IEnumerator ResetEnemy()
    {
        yield return new WaitForSeconds(10);
        fbx.GetComponent<Animator>().Play("standing up");
        GetComponent<Collider>().enabled = true;
        qte.enabled = true;
        timebar.enabled = true;
        navmesh.enabled = true;
        ai.enabled = true;
        stunned = false;
        yield return new WaitForSeconds(2);
        fbx.GetComponent<Animator>().Play("swagger");
    }
}
