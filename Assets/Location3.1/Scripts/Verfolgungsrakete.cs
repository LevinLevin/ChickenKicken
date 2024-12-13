using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verfolgungsrakete : MonoBehaviour, IPooledObject
{
    public float moveSpeed = 10f;

    public LayerMask whatIsPlayer;

    public string Tag;

    public ParticleSystem PSRauch;

    [Tooltip("Das Raketen Objekt")]
    public GameObject Rakete;

    Rigidbody rb;

    GameObject target; //Objekt mit Tag ist target

    Vector3 moveDirection;

    SchadenNehmen sn;
    bool getroffen;

    public void OnObjectSpawn() //On object spawn ist die start funktion für objekte aus dem objekt pool
    {
        sn = FindObjectOfType<SchadenNehmen>();

        rb = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag(Tag); //Das Huhn braucht den Player Tag um von der bullet gefunden zu werden

        Rakete.SetActive(true);

        if(target !=null )
        {
            moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
            Debug.Log("Target gefunden");
        }

        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

        PSRauch.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        getroffen = Physics.CheckSphere(transform.position, 1.5f, whatIsPlayer);

        StartCoroutine(death());
    }

    IEnumerator death()
    {
        if(getroffen)
        {
            sn.Autsch();
        }
        Rakete.SetActive(false);
        PSRauch.Stop();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
