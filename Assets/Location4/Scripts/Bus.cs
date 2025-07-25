using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour, IPooledObject
{
    //diese objecte müssen random platziert werden im bus
    private int entscheider; //dieser int entscheidet über das object
    public GameObject Skateboard1, Skateboard2; //sind für die punkte in dem spiel verantwortlich
    public GameObject Coin, Coin2; //tasche ist eine hürde über die man springen muss

    private float speed = 5.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        //objekte im buss müssen zuerst nicht da sein und werden erst später angezeigt
        Skateboard1.SetActive(false);
        Skateboard2.SetActive(false);

        Coin.SetActive(false);
        Coin2.SetActive(false);

        //rb = this.GetComponent<Rigidbody>();
        //rb.velocity = new Vector3(0, 0, -speed);
        Entscheide();
    }

    //private void Update()
    //{
    //    if(transform.position.z < -25f)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < -25f)
        {
            gameObject.SetActive(false);
        }
    }

    private void Entscheide()
    {
        entscheider = Random.Range(1, 5);
        Debug.Log("entscheider " + entscheider);
        //das hat der entscheider entschieden:
        switch(entscheider)
        {
            case 1:
                Skateboard1.SetActive(true);
                Coin2.SetActive(true);
                break;
                case 2:
                Coin.SetActive(true);
                Coin2.SetActive(true);
                break;
                case 3:
                Skateboard1.SetActive(true);
                break;
                case 4:
                Skateboard2.SetActive(true);
                break;
        }
    }
}
