using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kleidung : MonoBehaviour
{
    public GameObject KleidungsStueck;
    int points;
    public int kosten = 10;


    private void Start()
    {
    }
    private void Update()
    {
        Anziehen();
    }

    void Anziehen()
    {
        if (points >= kosten)
        {
            KleidungsStueck.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
