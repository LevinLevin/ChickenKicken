using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlugzeug : MonoBehaviour
{
    ObjectPooler objectPooler; //der Pool
    public string Flugzeug1;

    //public GameObject Flugzeug;

    public float respawnTime = 1.0f;
    private Vector2 screenBounds;
    public float IncreaseRespawnTime =100f;

    bool gameIsRunning = true;


    // Use this for initialization
    void Start()
    {
        //für den pool
        objectPooler = ObjectPooler.Instance;

        StartCoroutine(asteroidWave());
    }

    private void spawnEnemy()
    {
        objectPooler.SpawnFromPool(Flugzeug1, new Vector3(-45, 20, Random.Range(3, 20)), Quaternion.Euler(0,90,0f));
        
        /*GameObject a = Instantiate(Flugzeug) as GameObject;
        a.transform.position = new Vector3(-40, Random.Range(5, 8),Random.Range(3,20));*/
    }

    IEnumerator asteroidWave()
    {
        while (gameIsRunning ==true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
            if(respawnTime > 1.5)
            {
                respawnTime -= IncreaseRespawnTime * Time.deltaTime;
            }
        }
    }
}
