using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;

    public Text geldText;
    public static int geld;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        geld = PlayerPrefs.GetInt("AnzahlDesGeldes", 0);

        geldText.text = geld.ToString() + " Gold";
    }

    public void AddGold(int pGeld)
    {
        geld += pGeld;
        geldText.text = geld.ToString() + " Gold";
    }

    public int GetGold()
    {
        return geld;
    }

    public void RemoveGold(int pGeld)
    {
        geld -= pGeld;
        geldText.text = geld.ToString() + " Gold";
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("AnzahlDesGeldes", geld);
    }
}
