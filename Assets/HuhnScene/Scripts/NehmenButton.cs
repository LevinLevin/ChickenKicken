using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NehmenButton : MonoBehaviour
{
    public GameObject GoldImage;

    //für die particles
    public ParticleSystem Muenzen;
    public bool once = true;

    GoldManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GoldManager>();
    }

    public void CloseCanvas()
    {
        GoldImage.SetActive(false);
        Kaboom();

        if(gm != null)
        {
            gm.AddGold(10);
        }
    }

    private void Kaboom()
    {
        var em = Muenzen.emission;

        em.enabled = true;
        Muenzen.Play();

        once = false;
    }
}
