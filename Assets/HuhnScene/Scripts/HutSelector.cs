using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutSelector : MonoBehaviour
{
    public int currentHutIndex;
    public GameObject[] hute;
    // Start is called before the first frame update
    void Start()
    {
        currentHutIndex = PlayerPrefs.GetInt("SelectedHut", 0);
        foreach (GameObject hut in hute)
        {
            hut.SetActive(false);
        }

        hute[currentHutIndex].SetActive(true);
    }
}
