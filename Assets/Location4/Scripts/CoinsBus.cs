using UnityEngine;

public class CoinsBus : MonoBehaviour
{
    ScoreManager sm;

    private void Start()
    {
        sm = ScoreManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        sm.AddPoint(303);

        gameObject.SetActive(false);
    }
}
