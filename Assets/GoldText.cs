using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldText : MonoBehaviour
{
    public GameObject GoldImage;

    //für die particles
    public ParticleSystem Particle;
    public bool once = true;


    private void Start()
    {
        GoldImage.SetActive(false);
    }

    public void ShowImage()
    {
        //particles triggern
        Kaboom();
        //TextAnzeigen
        GoldImage.SetActive(true);
        LeanTween.scale(GoldImage, new Vector3(1.05f, 1.05f, 1.05f), 0.5f).setEase(LeanTweenType.easeInOutBounce);
        LeanTween.scale(GoldImage, new Vector3(1f, 1f, 1f), 0.7f).setDelay(0.6f).setEase(LeanTweenType.easeOutBack);

    }

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
