using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Geld : MonoBehaviour
{
    public static Geld instance;

    public Text GeldText;
    int anzahl = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GeldText.text = anzahl.ToString();
    }

    public void AddGeld()
    {
        anzahl += 20;
        GeldText.text = anzahl.ToString();
    }

}
