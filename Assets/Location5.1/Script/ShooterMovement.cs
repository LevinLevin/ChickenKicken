using UnityEngine;

public class ShooterMovement : MonoBehaviour
{
    public int desiredLane = 1;
    public float laneDistance = 4;

    [SerializeField] public Rigidbody rb;
    public float distToGround = 0.5f;
    private Vector3 targetPosition;

    void Update()
    {
        targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        else if (desiredLane == 3)
        {
            targetPosition += Vector3.right * 2 * laneDistance;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 4.5f * Time.deltaTime);
    }

    public void NachRechts()
    {
        desiredLane++;
        if (desiredLane == 4)
        {
            desiredLane = 3;
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
}
