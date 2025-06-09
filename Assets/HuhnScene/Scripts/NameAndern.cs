using UnityEngine;

public class NameAndern : MonoBehaviour
{
    public GameObject NameEingabeFeld;
    public GameObject RestlicheCanvas;

    TouchScreenKeyboard Tastatur;


    // Start is called before the first frame update
    void Start()
    {
        NameEingabeFeld.SetActive(false);
        RestlicheCanvas.SetActive(true);
    }

    public void andern()
    {
        Tastatur = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);

        NameEingabeFeld.SetActive(true);
        RestlicheCanvas.SetActive(false);
    }
}
