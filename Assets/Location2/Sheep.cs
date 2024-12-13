using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public static Sheep instance;

    public Rigidbody rb;
    public float distToGround = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //private void FixedUpdate()
    
        //Debug.Log(isGrounded());

        //if(isGrounded())
        
            //Jump();
        
    

    public void Jump()
    {
        Debug.Log(isGrounded());
        if (isGrounded())
        {
            Vector3 jumpVelocity = new Vector3(0, 4f, 0);
            rb.velocity = rb.velocity + jumpVelocity;
        }
    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }
}
