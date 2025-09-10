using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : MonoBehaviour
{
    NavMeshAgent ai;
    public List<Transform> points = new List<Transform>();
    int randpoint;
    Vector3 destination;

    void Start()
    {
        ai = GetComponent<NavMeshAgent>();
        Invoke(nameof(AIWalk), 2f);

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
        RandomPoint();
        ai.SetDestination(destination);
    }

    async private void Update()
    {
        if (!ai.pathPending && ai.remainingDistance <= ai.stoppingDistance)
        {
           await Task.Delay(5000); // aq teria uma animação do inimigo em idle fazendo nada
            AIWalk();
        }
    }
}