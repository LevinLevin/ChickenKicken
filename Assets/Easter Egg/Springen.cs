using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springen : MonoBehaviour
{
    public float distToGround = 0.5f;
    Rigidbody rb;
    public GameObject sheep;

    // Start is called before the first frame update
    void Start()
    {
        rb = sheep.GetComponent<Rigidbody>();
        StartCoroutine(Springenschleife());
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }

    IEnumerator Springenschleife()
    {
        while(true)
        {
            Vector3 jumpVelocity = new Vector3(0, 2f, 0);
            rb.linearVelocity = rb.linearVelocity + jumpVelocity;
            yield return new WaitForSeconds(2f);
        }
    }
}
