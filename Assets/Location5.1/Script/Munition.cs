using System;
using System.Collections;
using UnityEngine;

public class Munition : MonoBehaviour
{
    public static event Action munitionCollected;

    bool ammoCollected;
    float zeit;

    [SerializeField]
    GameObject munitionObj;

    [SerializeField]
    ParticleSystem dust;

    private bool playerInsideTrigger = false;

    private void Start()
    {
        ammoCollected = true;
        StartCoroutine(HideMunition());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;

            if (!ammoCollected)
            {
                ammoCollected = true; //coin wurde collected

                dust.Play();

                //invoke event to collect munition
                munitionCollected?.Invoke();

                StartCoroutine(HideMunition());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
        }
    }

    IEnumerator HideMunition()
    {
        munitionObj.SetActive(false); //coin ist unsichtbar und kann nicht collected werden

        zeit = UnityEngine.Random.Range(7, 15);
        yield return new WaitForSecondsRealtime(zeit);

        // Jetzt zusätzlich warten, bis der Spieler raus ist
        yield return new WaitUntil(() => !playerInsideTrigger);

        munitionObj.SetActive(true); //nach 10 sekunden ist der coin wieder sichtbar
        ammoCollected = false; //coin kann wieder collected werden
    }
}
