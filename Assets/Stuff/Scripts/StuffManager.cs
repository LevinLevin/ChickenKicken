using UnityEngine;
using UnityEngine.UI;

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
        UpdateUI();
        StuffEvents.TriggerUIChange();
    }

    private void OnEnable()
    {
        StuffEvents.OnUIChange += UpdateUI;
    }

    private void OnDisable()
    {
        StuffEvents.OnUIChange -= UpdateUI;
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
            UpdateUI();
            StuffEvents.TriggerUIChange();

            return;
        }

        PlayerPrefs.SetInt("SelectedHut", currentKopf);
        UpdateUI();
        StuffEvents.TriggerUIChange();
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
        UpdateUI();
        StuffEvents.TriggerUIChange();
    }

    private void UpdateUI()
    {
        erwerben.interactable = false;

        HutBlueprint c = platz[currentKopf];
        if (c.isUnlocked)
        {
            erwerben.gameObject.SetActive(false);
        }
        else
        {
            erwerben.gameObject.SetActive(true);
            kaufen.text = "PURCHASE " + c.price;
            if (c.price <= gm.GetGold())
            {
                erwerben.interactable = true;
            }
            else
            {
                erwerben.interactable = false;
            }
        }

        //gold kaufen möglich oder nicht
        if (sm.GetScore() > 10000)
        {
            goldKaufen.interactable = true;
        }
        else
        {
            goldKaufen.interactable = false;
        }

        //ability
        Abilities.SetHutBlueprint(c);
    }

    public void GoldKaufen()
    {
        if (sm == null || gm == null)
            return;

        if (sm.GetScore() >= 10000)
        {
            sm.RemovePoint(10000);
            sm.SaveScore();

            gm.AddGold(10);
        }

        UpdateUI();
        StuffEvents.TriggerUIChange();
    }
}
