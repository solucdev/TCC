using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : MonoBehaviour
{
    NavMeshAgent ai;
    public List<Transform> points = new List<Transform>();
    [SerializeField] int idleTime;
    [SerializeField] int difficult;
    [SerializeField] Transform player;

    int times;
    int randpoint;
    bool fp;
    Vector3 destination;

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        Invoke(nameof(AIWalk), 2f);
    }
    async private void Update()
    {
        if (!ai.pathPending && ai.remainingDistance <= ai.stoppingDistance || !fp)
        {
            await Task.Delay(idleTime * 1000); // aq teria uma animação do inimigo em idle fazendo nada
            AIWalk();
        }
        if(!ai.pathPending && ai.remainingDistance <= ai.stoppingDistance || fp)
        {
            FollowAgain(difficult);
        }
    }

    void RandomPoint()
    {
        randpoint = Random.Range(0, points.Count);
        destination = new Vector3(points[randpoint].position.x, points[randpoint].position.y, points[randpoint].position.z);
        int s = randpoint + 1;
        Debug.LogWarning("Inimigo está indo para o point" + s);
    }

    void AIWalk()
    {
        if (!fp)
        { RandomPoint();
        ai.SetDestination(destination); }
    }

    public void FollowPlayer()
    {
        fp = true;
        StartCoroutine(Recalculate());
        Debug.Log("é para seguir o jogador");

    }

    void FollowAgain(int difficult)
    {
        StartCoroutine(Recalculate());
        times++;
        if(times >= difficult)
        {
            fp = false;
            AIWalk();
        }
    }

    IEnumerator Recalculate()
    {
        yield return new WaitForSeconds(0.01f);
        ai.SetDestination(player.position);
    }
}