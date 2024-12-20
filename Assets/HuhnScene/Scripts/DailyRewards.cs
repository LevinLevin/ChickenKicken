﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyRewards : MonoBehaviour
{
    //for the timer
    public Text Time;
    public float msToWait;
    public Button ClickButton;
    private ulong lastTimeClicked;

    //for the rewards 
    public GameObject RewardCanvas;
    public GameObject CanvasEi;

    private void Start()
    {
        //RewardCanvas.SetActive(false);

        lastTimeClicked = ulong.Parse(PlayerPrefs.GetString("LastTimeClicked"));

        if (!Ready())
        {
            ClickButton.interactable = false;
        }
    }

    private void Update()
    {
        if (!ClickButton.IsInteractable())
        {
            if (!Ready())
            {
                RewardCanvas.SetActive(false);
                CanvasEi.SetActive(false);
            }
            else if (Ready())
            {
                RewardCanvas.SetActive(true);
                CanvasEi.SetActive(true);
                ClickButton.interactable = true;
                Time.text = "Ei!";
                return;
            }
            ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
            ulong m = diff / TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(msToWait - m) / 1000.0f;

            string r = "";
            //HOURS
            r += ((int)secondsLeft / 3600).ToString() + "h";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            //MINUTES
            r += ((int)secondsLeft / 60).ToString("00") + "m ";
            //SECONDS
            r += (secondsLeft % 60).ToString("00") + "s";
            Time.text = r;


        }
    }


    public void Click()
    {

        lastTimeClicked = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString("LastTimeClicked", lastTimeClicked.ToString());
        ClickButton.interactable = false;


    }
    private bool Ready()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - lastTimeClicked);
        ulong m = diff / TimeSpan.TicksPerMillisecond;

        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            //DO SOMETHING WHEN TIMER IS FINISHED
            return true;
        }

        return false;
    }
}
