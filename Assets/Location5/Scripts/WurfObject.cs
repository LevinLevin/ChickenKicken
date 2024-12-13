using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WurfObject : MonoBehaviour, IPooledObject
{
    public float moveSpeed = 10f;

    Rigidbody rb;
    BoxCollider bc;

    GameObject target; //Objekt mit Tag ist target

    public string Tag;

    Vector3 moveDirection;

    public LayerMask whatIsPlayer;

    bool getroffen;

    // From the interface  IPooledObject to play whenever is enable
    public void OnObjectSpawn()
    {
        rb = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag(Tag); //Das Huhn braucht den Player Tag um von der bullet gefunden zu werden

        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;

        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

        tag = "Player";

        bc = gameObject.GetComponent<BoxCollider>();
        bc.isTrigger = true;

        //Destroy(gameObject, 3f);
    }


    #region Collider
    private void OnTriggerEnter(Collider other)
    {
        getroffen = Physics.CheckSphere(transform.position, 1f, whatIsPlayer);

        if(getroffen)
        {
            bc.isTrigger = false;
            Invoke(nameof(Entfernen), 2f);
        }
        else
        {
            Invoke(nameof(Kaputt), 4f);
        }
    }

    void Entfernen()
    {
        gameObject.SetActive(false);
    }
    void Kaputt()
    {
        gameObject.SetActive(false);
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    //Code von: Alexander Zotov auf Youtube "How to make an enemy to aim and to fire bullet towards player position"
}
