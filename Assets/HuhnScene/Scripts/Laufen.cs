using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Laufen : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    private bool amLaufen = false;
    private bool drehtLinks = false;
    private bool drehtRechts = false;
    private bool amGehen = false;

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
    }

    void Update()
    {
        //if(amLaufen == false) //wenn es am laufen ist, soll es laufen
        //{
        //    StartCoroutine(Wandern());
        //}
        //if(drehtRechts == true)
        //{
        //    transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        //}
        //if (drehtLinks == true)
        //{
        //    transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        //}
        //if (amGehen == true)
        //{
        //    animator.SetBool("IsMoving", true);
        //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //}
        //else
        //{
        //    animator.SetBool("IsMoving", false);
        //}


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

    IEnumerator Wandern()
    {
        int rotTime = Random.Range(3, 6); //die Zeit die es gedreht wird
        int rotateWait = Random.Range(1, 3); //warte zeit bis rotieren 
        int rotateLoR = Random.Range(1, 3); //links oder rechts rotieren
        int walkWait = Random.Range(5, 7); //die Zeit zwischen dem laufen
        int walkTime = Random.Range(7, 10); // die zeit die es läuft

        amLaufen = true;

        yield return new WaitForSeconds(walkWait);
        amGehen = true;
        yield return new WaitForSeconds(walkTime);
        amGehen = false;
        yield return new WaitForSeconds(rotateWait);

        if(rotateLoR == 2)
        {
            drehtRechts = true;
            yield return new WaitForSeconds(rotTime);
            drehtRechts = false;
        }

        if (rotateLoR == 1)
        {
            drehtLinks = true;
            yield return new WaitForSeconds(rotTime);
            drehtLinks = false;
        }
        amLaufen = false;
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
