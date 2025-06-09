using UnityEngine;
using UnityEngine.UI;

public class namenbestatigen : MonoBehaviour
{
    public Text Name;
    public InputField eingabe;

    public GameObject NameEingabeFeld;
    public GameObject RestlicheCanvas;

    public void Awake()
    {
        Name.text = PlayerPrefs.GetString("NameDesHuhn");
        eingabe.text = PlayerPrefs.GetString("NameDesHuhn");
    }

    public void GutSo()
    {
        if (string.IsNullOrWhiteSpace(eingabe.text))
        {
            NameEingabeFeld.SetActive(false);
            RestlicheCanvas.SetActive(true);
            return;
        }

        //das name feld bekommt die schrift vom eingabefeld
        Name.text = eingabe.text;
        //der name wird gespeichert 
        PlayerPrefs.SetString("NameDesHuhn", Name.text);
        PlayerPrefs.Save();
        //die canvas wird deaktiviert
        NameEingabeFeld.SetActive(false);
        RestlicheCanvas.SetActive(true);

    }
}
