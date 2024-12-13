using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werbung : MonoBehaviour
{
    public GameObject WerbeFlugzeug;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalX(WerbeFlugzeug, 100, 20f);
    }
}
