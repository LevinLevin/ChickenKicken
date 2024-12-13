using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrone : MonoBehaviour
{
    float speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
