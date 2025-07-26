using UnityEngine;

public class NeuPunkte : MonoBehaviour
{
    ScoreManager sM;

    private void Start()
    {
        sM = ScoreManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(sM != null)
        {
            sM.AddPoint(2);
        }
    }
}
