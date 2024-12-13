using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySelector : MonoBehaviour
{
    public int currentBackIndex;
    public GameObject[] backpacks;
    // Start is called before the first frame update
    void Start()
    {
        currentBackIndex = PlayerPrefs.GetInt("SelectedBody", 0);
        foreach (GameObject backpack in backpacks)
        {
            backpack.SetActive(false);
        }

        backpacks[currentBackIndex].SetActive(true);
    }
}
