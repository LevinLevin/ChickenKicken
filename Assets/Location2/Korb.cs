using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Korb : MonoBehaviour
{
    public float distToGround = 0.5f;
    Rigidbody rb, rb2, rb3, rb4;
    public GameObject sheep, sheep2, sheep3, sheep4;

    //für die speech bubbles
    public int aktuelleSprechblase = 0;
    public GameObject[] bubbles;
    private bool einmal = false;

    ScoreManager sm;

    private void Start()
    {

        sm = FindObjectOfType<ScoreManager>();

        //alle sprechblasen verstecken
        foreach (GameObject bubble in bubbles)
        {
            bubble.SetActive(false);
        }

        //für die schafe
        rb = sheep.GetComponent<Rigidbody>();
        rb2 = sheep2.GetComponent<Rigidbody>();
        rb3 = sheep3.GetComponent<Rigidbody>();
        rb4 = sheep4.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Debug.Log(isGrounded());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(sm != null)
        {
            sm.AddPoint(73);
        }

        //PlayerPrefs.SetInt("AnzahlDerPunkte", PlayerPrefs.GetInt("AnzahlDerPunkte", 0) + 73);

        Jump();
        if (einmal == false)
        {
            StartCoroutine(bubbleAktivieren());
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }

    public void Jump()
    {
       // if (isGrounded())
        //{
            Vector3 jumpVelocity = new Vector3(0, 2f, 0);
            rb.velocity = rb.velocity + jumpVelocity;
        rb2.velocity = rb.velocity + jumpVelocity;
        rb3.velocity = rb.velocity + jumpVelocity;
        rb4.velocity = rb.velocity + jumpVelocity;
        //}
    }

    IEnumerator bubbleAktivieren()
    {
        einmal = true;
        aktuelleSprechblase = Random.Range(0, bubbles.Length +1);
        bubbles[aktuelleSprechblase].SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        bubbles[aktuelleSprechblase].SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        einmal = false;
    }
}
