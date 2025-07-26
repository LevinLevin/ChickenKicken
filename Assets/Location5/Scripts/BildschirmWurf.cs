using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BildschirmWurf : MonoBehaviour, IPooledObject
{
    float speed = 0.5f;
    Vector3 flugrichtung;
    GameObject target;
    Rigidbody rb;

    public void OnObjectSpawn()
    {
        target= GameObject.FindGameObjectWithTag("Player");
        flugrichtung = target.GetComponentInChildren<huhnWurf>().getflug(flugrichtung);
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.linearVelocity = flugrichtung * speed;
    }
}
