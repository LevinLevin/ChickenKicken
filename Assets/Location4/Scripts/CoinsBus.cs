using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsBus : MonoBehaviour
{
    ScoreManager sm;

    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(sm != null)
        {
            sm.AddPoint(147);
        }

        //PlayerPrefs.SetInt("AnzahlDerPunkte", PlayerPrefs.GetInt("AnzahlDerPunkte", 0) + 147);
        Destroy(gameObject);
    }
}
