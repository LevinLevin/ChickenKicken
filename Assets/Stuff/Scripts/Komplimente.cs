using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Komplimente : MonoBehaviour
{
    public int aktuelleSprechblase = 0;

    public GameObject[] bubbles;
    private int anzahlDerBlasen = 8;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject bubble in bubbles)
        {
            bubble.SetActive(false);
        }
        StartCoroutine(bubbleAktivieren());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator bubbleAktivieren()
    {
        bubbles[aktuelleSprechblase].SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        bubbles[aktuelleSprechblase].SetActive(false);
        while (true)
        {
            yield return new WaitForSecondsRealtime(10f);
            aktuelleSprechblase = Random.Range(1, anzahlDerBlasen);
            bubbles[aktuelleSprechblase].SetActive(true);
            yield return new WaitForSecondsRealtime(4f);
            bubbles[aktuelleSprechblase].SetActive(false);
            yield return new WaitForSecondsRealtime(10f);
            aktuelleSprechblase = Random.Range(1, anzahlDerBlasen);
            bubbles[aktuelleSprechblase].SetActive(true);
            yield return new WaitForSecondsRealtime(4f);
            bubbles[aktuelleSprechblase].SetActive(false);
            yield return new WaitForSecondsRealtime(10f);
            aktuelleSprechblase = Random.Range(1, anzahlDerBlasen);
            bubbles[aktuelleSprechblase].SetActive(true);
            yield return new WaitForSecondsRealtime(4f);
            bubbles[aktuelleSprechblase].SetActive(false);
            yield return new WaitForSecondsRealtime(10f);
            aktuelleSprechblase = Random.Range(1, anzahlDerBlasen);
            bubbles[aktuelleSprechblase].SetActive(true);
            yield return new WaitForSecondsRealtime(4f);
            bubbles[aktuelleSprechblase].SetActive(false);
        }

    }
}
