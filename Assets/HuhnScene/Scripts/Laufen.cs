using UnityEngine;
using UnityEngine.AI;

public class Laufen : MonoBehaviour
{
    public float maxDistX, minDistX, maxDistZ, minDistZ;

    public float maxCutoff;
    public float minCutoff;

    public float maxWaitTime;

    public float cancelDist;

    private bool isWandering;

    private Vector3 wanderPoint;

    private NavMeshAgent agent;

    private float waitTime;

    private float cutoff;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        waitTime = Random.Range(0, maxWaitTime);
        cutoff = Random.Range(0, maxCutoff);

        transform.position = new Vector3(Random.Range(-19, 16), 1, Random.Range(0, 7));
    }

    void Update()
    {
        if(!isWandering)
        {
            waitTime -= Time.deltaTime;
        }

        if(!isWandering && waitTime <= 0) 
        {
            Wander();
        }

        if(isWandering)
        {
            cutoff -= Time.deltaTime;
        }

        if(isWandering && Vector3.Distance(transform.position,wanderPoint) <= cancelDist || cutoff <= 0)
        {
            isWandering = false;
            agent.SetDestination(transform.position);
            animator.SetBool("IsMoving", false);
        }
    }

    void Wander()
    {
        animator.SetBool("IsMoving", true);
        isWandering = true;
        float rx = Random.Range(minDistX, maxDistX);
        float rz = Random.Range(minDistZ, maxDistZ);

        wanderPoint = new Vector3(transform.position.x + rx, 1, transform.position.z + rz);
        agent.SetDestination(wanderPoint);

        waitTime = Random.Range(0, maxWaitTime);
        cutoff = Random.Range(minCutoff, maxCutoff);
    }
}
