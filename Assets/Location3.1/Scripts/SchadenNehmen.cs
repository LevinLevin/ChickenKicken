using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchadenNehmen : MonoBehaviour
{
    public static SchadenNehmen instance;

    //schaltet die GameOver Canvas an 
    public GameObject GameOverCanvas;

    //macht nen sound
    public AudioSource huhnSound;

    //leben
    private int leben;

    private void Start()
    {
        huhnSound = GetComponent<AudioSource>();

        if(PlayerPrefs.GetInt("AbilityNumber", 0) == 1)
        {
            leben = 6;
            Debug.Log("Mehr leben dank ability");
        }
        else
        {
            leben = 3;
        }
    }

    private void Update()
    {
        Debug.Log(leben);
        if(leben <= 0)
        {
            GameOverCanvas.SetActive(true);
        }
    }

    public void Autsch()
    {
        huhnSound.Play();
        leben --;
    }
}
