using UnityEngine;

public class VasePhysic : MonoBehaviour
{
    public GameObject Vase;

    private void OnTriggerEnter(Collider other)
    {
        BreakIt();
    }

    public void BreakIt()
    {
        Instantiate(Vase, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
