using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandBus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerPrefs.GetInt("AbilityNumber", 0) == 2)
        {
            //weil die ability true ist, muss die anzahl an schaden erhöht werden um das spiel zu beenden
            PlayerPrefs.SetInt("AnzahlDerTN", 6);
        }
        else
        {
            PlayerPrefs.SetInt("AnzahlDerTN", 3);
        }
    }
}
