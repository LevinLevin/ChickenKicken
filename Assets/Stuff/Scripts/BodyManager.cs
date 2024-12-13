using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BodyManager : MonoBehaviour
{
    public int currentBody = 0;
    public GameObject[] korper;

    public BodyBlueprint[] Buddys;
    public Button erwerben;
    public Text kaufen;

    GoldManager gm;


    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GoldManager>();

        foreach (BodyBlueprint body in Buddys)
        {
            if (body.price == 0)
            {
                body.isUnlocked = true;
            }
            else
            {
                body.isUnlocked = PlayerPrefs.GetInt(body.name, 0) == 0 ? false : true;
            }
        }

        currentBody = PlayerPrefs.GetInt("SelectedBody", 0);
        foreach (GameObject location in korper)
        {
            location.SetActive(false);
        }

        korper[currentBody].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }

    public void nAnderes()
    {
        korper[currentBody].SetActive(false);

        currentBody++;
        if (currentBody == korper.Length)
        {
            currentBody = 0;
        }

        korper[currentBody].SetActive(true);
        BodyBlueprint b = Buddys[currentBody];
        if (!b.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedBody", currentBody);
    }

    public void Unlock()
    {
        BodyBlueprint b = Buddys[currentBody];

        PlayerPrefs.SetInt(b.name, 1);
        PlayerPrefs.SetInt("SelectedBody", currentBody);
        b.isUnlocked = true;

        if (gm != null)
        {
            gm.RemoveGold(b.price);
        }
    }

    private void updateUI()
    {
        BodyBlueprint b = Buddys[currentBody];
        if (b.isUnlocked)
        {
            erwerben.gameObject.SetActive(false);
        }
        else
        {
            erwerben.gameObject.SetActive(true);
            kaufen.text = "PURCHASE " + b.price;
            if (b.price <= PlayerPrefs.GetInt("AnzahlDesGeldes", 0))
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
