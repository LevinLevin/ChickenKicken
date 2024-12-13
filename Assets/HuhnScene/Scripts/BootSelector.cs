using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootSelector : MonoBehaviour
{
    public int currentBootIndex;
    public GameObject[] boots;
    // Start is called before the first frame update
    void Start()
    {
        currentBootIndex = PlayerPrefs.GetInt("SelectedBoot", 0);
        foreach (GameObject boot in boots)
        {
            boot.SetActive(false);
        }

        boots[currentBootIndex].SetActive(true);
    }
}
