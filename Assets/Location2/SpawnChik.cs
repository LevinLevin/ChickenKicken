using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChik : MonoBehaviour
{
    public GameObject Chicken;
    public Transform Spawnpoint;

    public bool isKorb;

    public Korb korb;

    void OnTriggerEnter(Collider others)
    {
        if (korb != null && !isKorb)
            korb.ResetCombo();

        Chicken.transform.position = Spawnpoint.transform.position;
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine()
    {
        holdon();
        yield return new WaitForSecondsRealtime(0.6f);
        okweiter();
    }

    void holdon()
    {
        Rigidbody Rigid = Chicken.GetComponent<Rigidbody>();
        Rigid.constraints = RigidbodyConstraints.FreezePosition;
    }

    void okweiter()
    {
        Rigidbody Rigid = Chicken.GetComponent<Rigidbody>();
        Rigid.constraints = RigidbodyConstraints.None;
    }

}
