using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Musik : MonoBehaviour
{
    public Text TxtMusikAus;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("MusikAus", 1) == 1)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public void MusikAus()
    {
        if (PlayerPrefs.GetInt("MusikAus", 1) == 0)
        {
            PlayerPrefs.SetInt("MusikAus", 1);
            TxtMusikAus.text = "Good Choice ;)";
        }
        else
        {
            PlayerPrefs.SetInt("MusikAus", 0);
            TxtMusikAus.text = "Restart the game to quit the song";
        }
        Debug.Log(PlayerPrefs.GetInt("MusikAus"));
    }
}
