using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlySpawn : MonoBehaviour
{
    ObjectPooler objectPooler;

    private float spawnzeit;

    public string spawnObject;

    public float minSpawnzeit = 2f;

    private int interval = 50;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;

        waitforspawn();

        spawnzeit = Random.Range(5f, 10f);
    }

    private void Update()
    {
        //die spawnzeit wird nur jedes xte frame berechnet
        if (Time.frameCount % interval == 0)
        {
            spawnzeit = Random.Range(minSpawnzeit, 20.0f);
        }
    }

    #region Spawn

    void spawn()
    {
        objectPooler.SpawnFromPool(spawnObject, transform.position, Quaternion.identity);
        Invoke(nameof(waitforspawn), 0.1f);
    }
    void waitforspawn()
    {
        Invoke(nameof(spawn), spawnzeit);
    }

    #endregion
}
