using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Muenze : MonoBehaviour
{
    public GameObject alterSchuh;
    public Text scoreText;
    public int score;

    private void OnTriggerEnter(Collider other)
    {
        Geld.instance.AddGeld();
    }

   
}
