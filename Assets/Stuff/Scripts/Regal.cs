using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regal : MonoBehaviour
{
    public GameObject[] kopf;
    public int currentKopf = 0;
    public HutBlueprint[] platz;

    public int currentBoot = 0;
    public GameObject[] boot;
    public BootBlueprint[] Schuhe;

    public int currentBody = 0;
    public GameObject[] korper;
    public BodyBlueprint[] Buddys;

    // Start is called before the first frame update
    void Start()
    {
        foreach (HutBlueprint hut in platz)
        {
            hut.isUnlocked = PlayerPrefs.GetInt(hut.name, 0) == 0 ? false : true;
        }

        foreach (BootBlueprint schuh in Schuhe)
        {
            schuh.isUnlocked = PlayerPrefs.GetInt(schuh.name, 0) == 0 ? false : true;
        }

        foreach (BodyBlueprint body in Buddys)
        {
            body.isUnlocked = PlayerPrefs.GetInt(body.name, 0) == 0 ? false : true;
        }

        currentKopf = PlayerPrefs.GetInt("SelectedHut", 0);
        foreach (GameObject hut in kopf)
        {
            HutBlueprint c = platz[currentKopf];
            if (c.isUnlocked)
            {
                hut.SetActive(true);
            }
        }

        kopf[currentKopf].SetActive(false);


        currentBoot = PlayerPrefs.GetInt("SelectedBoot", 0);
        foreach (GameObject schuh in boot)
        {
            BootBlueprint a = Schuhe[currentBoot];
            if (a.isUnlocked)
            {
                schuh.SetActive(true);
            }
        }

        boot[currentBoot].SetActive(false);


        currentBody = PlayerPrefs.GetInt("SelectedBody", 0);
        foreach (GameObject location in korper)
        {
            BodyBlueprint b = Buddys[currentBody];
            if (b.isUnlocked)
            {
                location.SetActive(false);
            }
        }

        korper[currentBody].SetActive(true);
    }

    // Wird von dem Wechseln button aufgerufen
    public void UpdateUI()
    {
        foreach (HutBlueprint hut in platz)
        {
            hut.isUnlocked = PlayerPrefs.GetInt(hut.name, 0) == 0 ? false : true;
        }

        foreach (BootBlueprint schuh in Schuhe)
        {
            schuh.isUnlocked = PlayerPrefs.GetInt(schuh.name, 0) == 0 ? false : true;
        }

        foreach (BodyBlueprint body in Buddys)
        {
            body.isUnlocked = PlayerPrefs.GetInt(body.name, 0) == 0 ? false : true;
        }

        currentKopf = PlayerPrefs.GetInt("SelectedHut", 0);
        foreach (GameObject hut in kopf)
        {
            HutBlueprint c = platz[currentKopf];
            if (c.isUnlocked)
            {
                hut.SetActive(true);
            }
        }

        kopf[currentKopf].SetActive(false);


        currentBoot = PlayerPrefs.GetInt("SelectedBoot", 0);
        foreach (GameObject schuh in boot)
        {
            BootBlueprint a = Schuhe[currentBoot];
            if (a.isUnlocked)
            {
                schuh.SetActive(true);
            }
        }

        boot[currentBoot].SetActive(false);


        currentBody = PlayerPrefs.GetInt("SelectedBody", 0);
        foreach (GameObject location in korper)
        {
            BodyBlueprint b = Buddys[currentBody];
            if (b.isUnlocked)
            {
                location.SetActive(false);
            }
        }

        korper[currentBody].SetActive(true);
    }
}
