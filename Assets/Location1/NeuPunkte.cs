using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuPunkte : MonoBehaviour
{
    ScoreManager sM;

    private void Start()
    {
        sM = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(sM != null)
        {
            sM.AddPoint(2);
        }
    }
}
