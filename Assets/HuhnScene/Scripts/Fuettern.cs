using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuettern : MonoBehaviour
{
    //for the particles
    public ParticleSystem Particle;
    public bool once = true;
    public Button futtern;


    private void Update()
    {
        if (PlayerPrefs.GetInt("AnzahlDesFutters", 0) >= 1)
        {
            futtern.interactable = true;
        }
        else
        {
            futtern.interactable = false;
        }
    }

    //particles
    public void Kaboom()
    {
        //es wird beim druecken ein wert vom futter abgezogen 
        PlayerPrefs.SetInt("AnzahlDesFutters", PlayerPrefs.GetInt("AnzahlDesFutters") - 1);

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
