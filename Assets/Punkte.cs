using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punkte : MonoBehaviour
{
    //Modifikations
    public GameObject KleidungsStueck;
    public GameObject alterSchuh;
    public GameObject alterSchuh2;
    public GameObject Rucksack;

    //Benötigte Punkte für die Aktivierung der Objekte
    int countPunkte;
    private int kostenPatHut = 500;
    private int kostenAlterSchuh = 1000;
    private int kostenRucksack = 2000;

    //Particles
    public ParticleSystem Particle;
    public bool once = true;

    private void Awake()
    {
        KleidungsStueck.SetActive(false);
        alterSchuh.SetActive(false);
        alterSchuh2.SetActive(false);
        Rucksack.SetActive(false);
        var em = Particle.emission;
        em.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(warte());
    }

    private void OnTriggerEnter(Collider other)
    {
        //ScoreManager.instance.AddPoint();
        countPunkte++;

        if (countPunkte == kostenPatHut/5)
        {
            AnziehenPatHut();
            Kaboom();
        }

        if (countPunkte == kostenAlterSchuh/5)
        {
            AnziehenAlterSchuh();
            Kaboom();
        }

        if (countPunkte == kostenRucksack/5)
        {
            AnziehenRucksack();
            Kaboom();
        }
    }

    void AnziehenPatHut()
    {
        //KleidungsStueck.GetComponent<MeshRenderer>().enabled = true;
        KleidungsStueck.SetActive(true);
    }

    void AnziehenAlterSchuh()
    {
        alterSchuh.SetActive(true);
        alterSchuh2.SetActive(true);
    }

    void AnziehenRucksack()
    {
        Rucksack.SetActive(true);
    }

    private IEnumerator warte()
    {
        yield return new WaitForSeconds(20);
    }

    //für das particle system
    private void Kaboom()
    {
        var em = Particle.emission;
        //var dur = Particle.duration;

        em.enabled = true;
        Particle.Play();
        //Particle.Stop();
        //em.enabled = false;

        once = false;
    }

}
