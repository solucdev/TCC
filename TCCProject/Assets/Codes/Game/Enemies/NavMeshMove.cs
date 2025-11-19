using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavMeshMove : MonoBehaviour
{
    NavMeshAgent ai;
    public List<Transform> points = new List<Transform>();
    [SerializeField] int idleTime;
    [SerializeField] int difficult;
    [SerializeField] Transform player;
    [SerializeField] GameObject fbx;

    [SerializeField] TypeWriter effect;
    [SerializeField] Text thought;

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
        if (!ai.pathPending && ai.remainingDistance <= ai.stoppingDistance && !fp)
        {
            fbx.GetComponent<Animator>().Play("idle"); 
            await Task.Delay(idleTime * 1000);
            AIWalk();
        }
        if(!ai.pathPending && ai.remainingDistance <= ai.stoppingDistance && fp)
        {
            FollowAgain(difficult);
        }

        if(ai.speed < 0.2f)
        {
            fbx.GetComponent<Animator>().Play("idle");
        }
    }

    void RandomPoint()
    {
        randpoint = Random.Range(0, points.Count);
        destination = new Vector3(points[randpoint].position.x, points[randpoint].position.y, points[randpoint].position.z);
        int s = randpoint + 1;
    }

    void AIWalk()
    {
        fbx.GetComponent<Animator>().Play("swagger");
        if (!fp)
        { RandomPoint();
        ai.SetDestination(destination); }
    }

    public void FollowPlayer()
    {
        fp = true;
        StartCoroutine(Recalculate());
        Debug.Log("visto");
        StartCoroutine(effect.Typer(thought, "você foi visto!"));
        StartCoroutine(effect.EraseMessage(thought, 2));
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