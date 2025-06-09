using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werbung : MonoBehaviour
{
    public GameObject WerbeFlugzeug;

    public GameObject[] banner;

    void Start()
    {
        LeanTween.moveLocalX(WerbeFlugzeug, 100, 20f);

        int ran = Random.Range(0, banner.Length);

        foreach(GameObject ban in banner) {
            ban.SetActive(false);
        }

        banner[ran].gameObject.SetActive(true);
    }
}
