using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    private Touch touch;
    private float speedModifier;
    public Rigidbody block;


    // Start is called before the first frame update
    void Start()
    {
        speedModifier = 0.005f;
    }

    void FixedUpdate()
    {
        Movement();
    }


    // Update is called once per frame
    public void Movement()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedModifier,
                    transform.position.y,
                    transform.position.z + touch.deltaPosition.y * speedModifier);
            }
        }
    }


}
