using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class BodyManager : MonoBehaviour
{
    public int currentBody = 0;
    public GameObject[] korper;

    public BodyBlueprint[] Buddys;
    public Button BtnErwerben;
    public Text kaufen;

    GoldManager gm;

    void Start()
    {
        gm = GoldManager.instance;

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
        UpdateUI();
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
            UpdateUI();
            StuffEvents.TriggerUIChange();
            return;
        }

        PlayerPrefs.SetInt("SelectedBody", currentBody);
        UpdateUI();
        StuffEvents.TriggerUIChange();
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
        UpdateUI();
        StuffEvents.TriggerUIChange();
    }

    private void UpdateUI()
    {
        BtnErwerben.interactable = false;

        BodyBlueprint b = Buddys[currentBody];
        if (b.isUnlocked)
        {
            BtnErwerben.gameObject.SetActive(false);
        }
        else
        {
            BtnErwerben.gameObject.SetActive(true);
            kaufen.text = "PURCHASE " + b.price;
            if (b.price <= gm.GetGold())
            {
                BtnErwerben.interactable = true;
            }
            else
            {
                BtnErwerben.interactable = false;
            }
        }
        //ability
        Abilities.SetBodyBlueprint(b);
    }
}
