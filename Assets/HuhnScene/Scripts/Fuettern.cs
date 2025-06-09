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

    //particles
    public void Kaboom()
    {
        //es wird beim druecken ein wert vom futter abgezogen 
        //PlayerPrefs.SetInt("AnzahlDesFutters", PlayerPrefs.GetInt("AnzahlDesFutters") - 1);

        //particles
        var em = Particle.emission;

        em.enabled = true;
        Particle.Play();
    }
}
