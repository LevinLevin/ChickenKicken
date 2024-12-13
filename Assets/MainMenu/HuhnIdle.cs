using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuhnIdle : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveLocalX(gameObject, -50f, speed);
    }
}
