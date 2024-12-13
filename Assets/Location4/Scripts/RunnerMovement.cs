using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    private int desiredLane = 1; //ein integer für jede linie 0 für links, 1 für mitte, usw
    public float laneDistance = 4; //die distanz zwischen den linien

    [SerializeField] public Rigidbody rb;
    public float distToGround = 0.5f;
    private Vector3 targetPosition;

    //das huhn soll sich nicht bewegen, bis es den ersten bus erreicht hat
    public bool isGestartet;
    [SerializeField] GameObject links, rechts; //das sind die buttons für das movement

    //für die Animationen
    [SerializeField] private Animator animator;

    private void Start()
    {
        //das huhn fliegt, bis es einen Bus erreicht
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        //das huhn bewegt sich nict, bis es einen Bus erreicht hat
        links.SetActive(false);
        rechts.SetActive(false);

        isGestartet= false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded())
        {
            animator.SetBool("isJumping", false);
            rb.isKinematic = false;
            isGestartet= true;
            if(isGestartet)
            {
                links.SetActive(true);
                rechts.SetActive(true);
            }
        }
        else if(!isGrounded())
        {
            animator.SetBool("isJumping", true);
        }
        targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 8 * Time.deltaTime);
    }

    public void NachRechts()
    {
        desiredLane++;
        if (desiredLane == 3)
        {
            desiredLane = 2;
        }
    }

    public void NachLinks()
    {
        desiredLane--;
        if (desiredLane == -1)
        {
            desiredLane = 0;
        }
    }

    public void Jump()
    {
        Debug.Log(isGrounded());
        if (isGrounded())
        {
            Vector3 jumpVelocity = new Vector3(0, 6f, 0);
            rb.velocity = rb.velocity + jumpVelocity;
        }
    }
    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }
}
