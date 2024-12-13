using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVerfolgungsrakete : MonoBehaviour
{
    ObjectPooler objectPooler; //der Pool

    public string VF;

    public GameObject Achtung;

    // Start is called before the first frame update
    void Start()
    {
        //für den pool
        objectPooler = ObjectPooler.Instance;

        StartCoroutine(Kill());

        LeanTween.scale(Achtung, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setEaseOutSine().setLoopPingPong();
    }

    IEnumerator Kill()
    {
        while(true)
        {
            Achtung.SetActive(true);
            yield return new WaitForSecondsRealtime(1.5f);
            spawn();
            Achtung.SetActive(false);
            yield return new WaitForSecondsRealtime(5f);
        }
    }

    void spawn()
    {
        objectPooler.SpawnFromPool(VF, transform.position, Quaternion.identity);
    }
}
