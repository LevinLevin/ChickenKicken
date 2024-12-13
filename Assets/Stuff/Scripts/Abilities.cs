using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    //shooteroutfit
    int Sschuh = 1;
    int Sbody = 1;
    int Shut = 4;

    //rodeo outfit
    int Rschuh = 3;
    int Rbody = 3;
    int Rhut = 1;

    //cross outfit
    int Cschuh = 6;
    int Chut = 6;
    int Cbody = 6;

    //moor outfit
    int Mschuh = 4;
    int Mhut = 3;
    int Mbody = 4;

    bool Rodeo1, Rodeo2, Rodeo3;
    bool Kong1, Kong2, Kong3;
    bool Cross1, Cross2, Cross3;
    bool Moor1, Moor2, Moor3;

    public Image BootImage, BodyImage, HutImage;

    private void Start()
    {
        BootImage.color= Color.white;
        BodyImage.color= Color.white;
        HutImage.color = Color.white;

        UpdateColor();
    }

    void Update()
    {
        UpdateAbility();
    }


    public void UpdateColor()
    {
        //check for shooter ability
        if (PlayerPrefs.GetInt("SelectedBody") == Sbody)
        {
            FarbeWechseln(BodyImage, Color.cyan);
            Kong1 = true;
            Rodeo1= false;
            Cross1= false;
            Moor1 = false;
        }
        else if(PlayerPrefs.GetInt("SelectedBody") == Rbody)
        {
            FarbeWechseln(BodyImage, Color.red);
            Rodeo1 = true;
            Kong1 = false;
            Cross1= false;
            Moor1 = false;
        }
        else if (PlayerPrefs.GetInt("SelectedBody") == Cbody)
        {
            FarbeWechseln(BodyImage, Color.blue);
            Cross1 = true;
            Kong1 = false;
            Rodeo1= false;
            Moor1 = false;
        }
        else if (PlayerPrefs.GetInt("SelectedBody") == Mbody)
        {
            FarbeWechseln(BodyImage, Color.yellow);
            Cross1 = false;
            Kong1 = false;
            Rodeo1 = false;
            Moor1= true;
        }
        else
        {
            FarbeWechseln(BodyImage, Color.white);
            Kong1 = false;
            Rodeo1= false;
            Cross1= false;
            Moor1 = false;
        }
        if (PlayerPrefs.GetInt("SelectedHut") == Shut)
        {
            FarbeWechseln(HutImage, Color.cyan);
            Kong2 = true;
            Cross2= false;
            Rodeo2= false;
            Moor2 = false;
        }
        else if (PlayerPrefs.GetInt("SelectedHut") == Rhut)
        {
            FarbeWechseln(HutImage, Color.red);
            Rodeo2 = true;
            Cross2= false;
            Kong2 = false;
            Moor2 = false;
        }
        else if (PlayerPrefs.GetInt("SelectedHut") == Chut)
        {
            FarbeWechseln(HutImage, Color.blue);
            Cross2 = true;
            Rodeo2= false;
            Kong2 = false;
            Moor2 = false;
        }
        else if (PlayerPrefs.GetInt("SelectedHut") == Mhut)
        {
            FarbeWechseln(HutImage, Color.yellow);
            Cross2 = false;
            Rodeo2 = false;
            Kong2 = false;
            Moor2= true;
        }
        else
        {
            FarbeWechseln(HutImage, Color.white);
            Kong2 = false;
            Rodeo2= false;
            Cross2= false;
            Moor2 = false;
        }
        if (PlayerPrefs.GetInt("SelectedBoot") == Sschuh){
            FarbeWechseln(BootImage, Color.cyan);
            Kong3 = true;
            Cross3= false;
            Rodeo3= false;
            Moor3 = false;
        }
        else if (PlayerPrefs.GetInt("SelectedBoot") == Rschuh)
        {
            FarbeWechseln(BootImage, Color.red);
            Rodeo3 = true;
            Kong3 = false;
            Cross3= false;
            Moor3 = false;
        }
        else if (PlayerPrefs.GetInt("SelectedBoot") == Cschuh)
        {
            FarbeWechseln(BootImage, Color.blue);
            Cross3 = true;
            Kong3 = false;
            Rodeo3= false;
            Moor3 = false;
        }
        else if (PlayerPrefs.GetInt("SelectedBoot") == Mschuh)
        {
            FarbeWechseln(BootImage, Color.yellow);
            Cross3 = false;
            Kong3 = false;
            Rodeo3 = false;
            Moor3= true;
        }
        else
        {
            FarbeWechseln(BootImage, Color.white);
            Kong3 = false;
            Rodeo3= false;
            Cross3= false;
            Moor3 = false;
        }
    }

    public void FarbeWechseln(Image image, Color color)
    {
        image.color = color;
    }

    void UpdateAbility()
    {
        if (Kong1 && Kong2 && Kong3)
        {
            PlayerPrefs.SetInt("AbilityNumber", 1);
            Debug.Log("KongAbility is true");
        }
        else if (Cross1 && Cross2 && Cross3)
        {
            PlayerPrefs.SetInt("AbilityNumber", 2);
            Debug.Log("CrossAbility is true");
        }
        else if (Rodeo1 && Rodeo2 && Rodeo3)
        {
            PlayerPrefs.SetInt("AbilityNumber", 3);
            Debug.Log("RodeoAbility is true");
        }
        else if(Moor1 && Moor2 && Moor3)
        {
            PlayerPrefs.SetInt("AbilityNumber", 4);
            Debug.Log("MoorAbility is true");
        }
        else
        {
            PlayerPrefs.SetInt("AbilityNumber", 0);
        }
    }
}
