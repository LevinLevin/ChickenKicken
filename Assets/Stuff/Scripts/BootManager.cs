using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class BootManager : MonoBehaviour
{
    public int currentBoot = 0;
    public GameObject[] boot;

    public BootBlueprint[] Schuhe;
    public Button erwerben;
    public Text kaufen;

    GoldManager gm;

    void Start()
    {
        gm= GoldManager.instance;

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
            UpdateUI();
            StuffEvents.TriggerUIChange();
            return;
        }
        PlayerPrefs.SetInt("SelectedBoot", currentBoot);

        //UI
        UpdateUI();
        StuffEvents.TriggerUIChange();
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

        //UI
        UpdateUI();
        StuffEvents.TriggerUIChange();
    }

    private void UpdateUI()
    {
        erwerben.interactable = false;

        BootBlueprint a = Schuhe[currentBoot];
        if (a.isUnlocked)
        {
            erwerben.gameObject.SetActive(false);
        }
        else
        {
            erwerben.gameObject.SetActive(true);
            kaufen.text = "PURCHASE " + a.price;
            if (a.price <= gm.GetGold())
            {
                erwerben.interactable = true;
            }
            else
            {
                erwerben.interactable = false;
            }
        }
        Abilities.SetBootBlueprint(a);
    }
}
