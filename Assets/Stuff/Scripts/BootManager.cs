using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BootManager : MonoBehaviour
{
    public int currentBoot = 0;
    public GameObject[] boot;

    public BootBlueprint[] Schuhe;
    public Button erwerben;
    public Text kaufen;

    GoldManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm= FindObjectOfType<GoldManager>();

        foreach (BootBlueprint schuh in Schuhe)
        {
            if (schuh.price == 0)
            {
                schuh.isUnlocked = true;
            }
            else
            {
                schuh.isUnlocked = PlayerPrefs.GetInt(schuh.name, 0) == 0 ? false : true;
            }
        }

        currentBoot = PlayerPrefs.GetInt("SelectedBoot", 0);
        foreach (GameObject schuh in boot)
        {
            schuh.SetActive(false);
        }

        boot[currentBoot].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }

    public void nAnderes()
    {
        boot[currentBoot].SetActive(false);

        currentBoot++;
        if (currentBoot == boot.Length)
        {
            currentBoot = 0;
        }

        boot[currentBoot].SetActive(true);
        BootBlueprint a = Schuhe[currentBoot];
        if (!a.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedBoot", currentBoot);
    }

    public void Unlock()
    {
        BootBlueprint a = Schuhe[currentBoot];

        PlayerPrefs.SetInt(a.name, 1);
        PlayerPrefs.SetInt("SelectedBoot", currentBoot);
        a.isUnlocked = true;

        if (gm != null)
        {
            gm.RemoveGold(a.price);
        }
    }

    private void updateUI()
    {
        BootBlueprint a = Schuhe[currentBoot];
        if (a.isUnlocked)
        {
            erwerben.gameObject.SetActive(false);
        }
        else
        {
            erwerben.gameObject.SetActive(true);
            kaufen.text = "PURCHASE " + a.price;
            if (a.price <= PlayerPrefs.GetInt("AnzahlDesGeldes", 0))
            {
                erwerben.interactable = true;
            }
            else
            {
                erwerben.interactable = false;
            }
        }

    }
}
