using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StuffManager : MonoBehaviour
{
    public int currentKopf = 0;
    public GameObject[] kopf;

    public HutBlueprint[] platz;
    public Button erwerben;
    public Text kaufen;
    //gold kaufen
    public Button goldKaufen;

    GoldManager gm;
    ScoreManager sm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GoldManager>();
        sm = FindObjectOfType<ScoreManager>();


        foreach (HutBlueprint hut in platz)
        {
            if (hut.price == 0)
            {
                hut.isUnlocked = true;
            }
            else
            {
                hut.isUnlocked = PlayerPrefs.GetInt(hut.name, 0) == 0 ? false : true;
            }
        }

        currentKopf = PlayerPrefs.GetInt("SelectedHut", 0);
        foreach (GameObject location in kopf)
        {
            location.SetActive(false);
        }

        kopf[currentKopf].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }

    public void nAnderes()
    {
        kopf[currentKopf].SetActive(false);

        currentKopf++;
        if (currentKopf == kopf.Length)
        {
            currentKopf = 0;
        }

        kopf[currentKopf].SetActive(true);
        HutBlueprint c = platz[currentKopf];
        if (!c.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedHut", currentKopf);
    }

    public void Unlock()
    {
        HutBlueprint c = platz[currentKopf];

        PlayerPrefs.SetInt(c.name, 1);
        PlayerPrefs.SetInt("SelectedHut", currentKopf);
        c.isUnlocked = true;

        if(gm != null)
        {
            gm.RemoveGold(c.price);
        }
    }

    private void updateUI()
    {
        HutBlueprint c = platz[currentKopf];
        if (c.isUnlocked)
        {
            erwerben.gameObject.SetActive(false);
        }
        else
        {
            erwerben.gameObject.SetActive(true);
            kaufen.text = "PURCHASE " + c.price;
            if (c.price <= PlayerPrefs.GetInt("AnzahlDesGeldes", 0))
            {
                erwerben.interactable = true;
            }
            else
            {
                erwerben.interactable = false;
            }
        }

        //gold kaufen möglich oder nicht
        if (PlayerPrefs.GetInt("AnzahlDerPunkte", 0) > 10000)
        {
            goldKaufen.interactable = true;
        }
        else
        {
            goldKaufen.interactable = false;
        }
    }

    public void GoldKaufen()
    {
        if (PlayerPrefs.GetInt("AnzahlDerPunkte", 0) >= 10000)
        {
            Debug.Log("Gold wurde gekauft");

            if(sm != null)
            {
                sm.RemovePoint(10000);
                sm.LoadScore();
            }
            //PlayerPrefs.SetInt("AnzahlDerPunkte", -10000);

            if(gm != null)
            {
                gm.AddGold(10);
            }
        }
    }
}
