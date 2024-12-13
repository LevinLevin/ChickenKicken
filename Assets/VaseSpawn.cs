using System.Collections;
using UnityEngine;

public class VaseSpawn : MonoBehaviour
{
    public GameObject Vase;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(7);
            SpawnVase();
        }
    }

    private void SpawnVase()
    {
        GameObject a = Instantiate(Vase);
        a.transform.position = new Vector3(Random.Range(-6, 8), -2.1f, Random.Range(3, -2.5f));
        Destroy(a , 20f);
    }
}
