using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zuruecksetzen : MonoBehaviour
{
    public GameObject schaf1,schaf2,schaf3,schaf4;
    public GameObject Chicken;
    public Transform SpC, Sp1, Sp2, Sp3, Sp4;


    void OnTriggerEnter(Collider others)
    {
        Chicken.transform.position = SpC.transform.position;
        StartCoroutine(Coroutine());
        schaf1.transform.position = Sp1.transform.position;
        schaf2.transform.position = Sp2.transform.position;
        schaf3.transform.position = Sp3.transform.position;
        schaf4.transform.position = Sp4.transform.position;
        StartCoroutine(Coroutine());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Coroutine()
    {
        holdon();
        yield return new WaitForSecondsRealtime(1.5f);
        okweiter();
    }

    void holdon()
    {
        Rigidbody Rigid = schaf1.GetComponent<Rigidbody>();
        Rigidbody Rigid2 = schaf2.GetComponent<Rigidbody>();
        Rigidbody Rigid3 = schaf3.GetComponent<Rigidbody>();
        Rigidbody Rigid4 = schaf4.GetComponent<Rigidbody>();
        Rigid.constraints = RigidbodyConstraints.FreezePosition;
        Rigid2.constraints = RigidbodyConstraints.FreezePosition;
        Rigid3.constraints = RigidbodyConstraints.FreezePosition;
        Rigid4.constraints = RigidbodyConstraints.FreezePosition;

    }

    void okweiter()
    {
        Rigidbody Rigid = schaf1.GetComponent<Rigidbody>();
        Rigidbody Rigid2 = schaf2.GetComponent<Rigidbody>();
        Rigidbody Rigid3 = schaf3.GetComponent<Rigidbody>();
        Rigidbody Rigid4 = schaf4.GetComponent<Rigidbody>();
        Rigid.constraints = RigidbodyConstraints.None;
        Rigid2.constraints = RigidbodyConstraints.None;
        Rigid3.constraints = RigidbodyConstraints.None;
        Rigid4.constraints = RigidbodyConstraints.None;
    }
}
