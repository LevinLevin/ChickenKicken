using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FZSchiesen : MonoBehaviour
{
    public GameObject BulletObject;
    public float schussSpeed;

    //bool um zu prüfen ob er schießen soll
    bool wach;

    //sound
    public AudioSource smallExplosion;

    private void Start()
    {
        Schiesen();
    }

    public void Schiesen()
    {
        schussSpeed = Random.Range(1f, 1.5f);
        if(gameObject.activeInHierarchy)
        {
            GameObject bullet = Instantiate(BulletObject, transform.position, transform.rotation);
            Destroy(bullet, 2f);
        }
        Invoke(nameof(Schiesen), schussSpeed);
    }
}
