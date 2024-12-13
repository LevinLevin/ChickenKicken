using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private Touch touch;
    private float speedModifier;
    public Rigidbody Camera;

    private float minX, maxX;


    // Start is called before the first frame update
    void Start()
    {
        speedModifier = 0.005f;

        minX = -11f;
        maxX = 11f;
    }

    void FixedUpdate()
    {
        Movement();
    }


    // Update is called once per frame
    public void Movement()
    {
        LockScreenBounds();

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedModifier,
                    transform.position.y,
                    transform.position.z);
            }
        }
    }

    private void LockScreenBounds()
    {
        Vector3 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x, minX, maxX);

        transform.position = playerPosition;
    }
}
