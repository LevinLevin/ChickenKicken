using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof (BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FixedJoystick joystick;

    [SerializeField] private float moveSpeed;

    //jump
    public float distToGround = 0.5f;

    //für die Animationen
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!isGrounded())
        {
            animator.SetBool("isJumping", true);
        }
    }

    public void Jump()
    {
        Debug.Log(isGrounded());
        if (isGrounded())
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        if (joystick.Horizontal != 0 || joystick.Vertical !=0)
        {
            animator.SetBool("isJumping", false);
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }



    //Joystick logic by Atlass on YT "How to Create Mobile Joystick in Unity 3D | Unity, Joystick, Tutorial, 2021"
}
