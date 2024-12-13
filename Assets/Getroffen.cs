using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getroffen : MonoBehaviour, IPooledObject
{
    private int treffer;

    //damit das Flugzeug nur von Patronen des Huhn getroffen werden kann
    public LayerMask PatroneL;

    //für die sounds
    public AudioSource explosionSound;

    //für die particles
    public ParticleSystem Particle;
    public bool once = true;

    //für die GameOver Canvas
    bool getroffen = false;
    bool einmal = true;

    ScoreManager sm;

    public void OnObjectSpawn()
    {
        getroffen = false;
        einmal = true;
    }

    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();

        explosionSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (getroffen == true && einmal == true)
        {
            PlayerPrefs.SetInt("AnzahlDerFZ", PlayerPrefs.GetInt("AnzahlDerFZ", 0) + 1);
            getroffen = false;
            einmal = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PatroneL == (PatroneL | (1 << other.gameObject.layer)))
        {
            //der Treffer wird gezählt
            getroffen = true;
            Kaboom();
            //der sound wird gespielt 
            if (PlayerPrefs.GetInt("MusikAus", 1) == 1)
            {
                explosionSound.Play();
            }

            //punkte werden addiert
            //PlayerPrefs.SetInt("AnzahlDerPunkte", PlayerPrefs.GetInt("AnzahlDerPunkte", 0) + 33);
            if (sm != null)
            {
                sm.AddPoint(33);
            }
        }
    }

    public void Kaboom()
    {

        //particles
        var em = Particle.emission;
        //var dur = Particle.duration;

        em.enabled = true;
        Particle.Play();
        //Particle.Stop();
        //em.enabled = false;

        once = false;
    }
}
