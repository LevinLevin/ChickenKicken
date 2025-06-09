using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Worst Script ever written
/// </summary>
public class Abilities : MonoBehaviour
{
    bool Hoop1, Hoop2, Hoop3;
    bool Kong1, Kong2, Kong3;
    bool Cross1, Cross2, Cross3;
    bool Moor1, Moor2, Moor3;

    static BodyBlueprint bodyBlueprint;
    static BootBlueprint bootBlueprint;
    static HutBlueprint hutBlueprint;

    public Image BootImage, BodyImage, HutImage;

    private void Start()
    {
        BootImage.color= Color.white;
        BodyImage.color= Color.white;
        HutImage.color = Color.white;
    }

    private void OnEnable()
    {
        StuffEvents.OnUIChange += UpdateColor;
    }

    private void OnDisable()
    {
        StuffEvents.OnUIChange -= UpdateColor;
    }

    public static void SetHutBlueprint(HutBlueprint hut)
    {
        hutBlueprint = hut;
    }

    public static void SetBodyBlueprint(BodyBlueprint body)
    {
        bodyBlueprint = body;
    }

    public static void SetBootBlueprint(BootBlueprint boot)
    {
        bootBlueprint = boot;
    }

    public void UpdateColor()
    {
        //check for shooter ability
        if (bodyBlueprint != null && bodyBlueprint.isUnlocked && bodyBlueprint.abilityGame == AbilityNames.KongGame)
        {
            FarbeWechseln(BodyImage, Color.cyan);
            Kong1 = true;
            Hoop1= false;
            Cross1= false;
            Moor1 = false;
        }
        else if(bodyBlueprint != null && bodyBlueprint.isUnlocked && bodyBlueprint.abilityGame == AbilityNames.HoopGame)
        {
            FarbeWechseln(BodyImage, Color.red);
            Hoop1 = true;
            Kong1 = false;
            Cross1= false;
            Moor1 = false;
        }
        else if (bodyBlueprint != null && bodyBlueprint.isUnlocked && bodyBlueprint.abilityGame == AbilityNames.CrossGame)
        {
            FarbeWechseln(BodyImage, Color.blue);
            Cross1 = true;
            Kong1 = false;
            Hoop1= false;
            Moor1 = false;
        }
        else if (bodyBlueprint != null && bodyBlueprint.isUnlocked && bodyBlueprint.abilityGame == AbilityNames.MoorGame)
        {
            FarbeWechseln(BodyImage, Color.yellow);
            Cross1 = false;
            Kong1 = false;
            Hoop1 = false;
            Moor1= true;
        }
        else
        {
            FarbeWechseln(BodyImage, Color.white);
            Kong1 = false;
            Hoop1= false;
            Cross1= false;
            Moor1 = false;
        }


        if (hutBlueprint != null && hutBlueprint.isUnlocked && hutBlueprint.abilityGame == AbilityNames.KongGame)
        {
            FarbeWechseln(HutImage, Color.cyan);
            Kong2 = true;
            Cross2= false;
            Hoop2= false;
            Moor2 = false;
        }
        else if (hutBlueprint != null && hutBlueprint.isUnlocked && hutBlueprint.abilityGame == AbilityNames.HoopGame)
        {
            FarbeWechseln(HutImage, Color.red);
            Hoop2 = true;
            Cross2= false;
            Kong2 = false;
            Moor2 = false;
        }
        else if (hutBlueprint != null && hutBlueprint.isUnlocked && hutBlueprint.abilityGame == AbilityNames.CrossGame)
        {
            FarbeWechseln(HutImage, Color.blue);
            Cross2 = true;
            Hoop2= false;
            Kong2 = false;
            Moor2 = false;
        }
        else if (hutBlueprint != null && hutBlueprint.isUnlocked && hutBlueprint.abilityGame == AbilityNames.MoorGame)
        {
            FarbeWechseln(HutImage, Color.yellow);
            Cross2 = false;
            Hoop2 = false;
            Kong2 = false;
            Moor2= true;
        }
        else
        {
            FarbeWechseln(HutImage, Color.white);
            Kong2 = false;
            Hoop2= false;
            Cross2= false;
            Moor2 = false;
        }


        if (bootBlueprint != null && bootBlueprint.isUnlocked && bootBlueprint.abilityGame == AbilityNames.KongGame){
            FarbeWechseln(BootImage, Color.cyan);
            Kong3 = true;
            Cross3= false;
            Hoop3= false;
            Moor3 = false;
        }
        else if (bootBlueprint != null && bootBlueprint.isUnlocked && bootBlueprint.abilityGame == AbilityNames.HoopGame)
        {
            FarbeWechseln(BootImage, Color.red);
            Hoop3 = true;
            Kong3 = false;
            Cross3= false;
            Moor3 = false;
        }
        else if (bootBlueprint != null && bootBlueprint.isUnlocked && bootBlueprint.abilityGame == AbilityNames.CrossGame)
        {
            FarbeWechseln(BootImage, Color.blue);
            Cross3 = true;
            Kong3 = false;
            Hoop3= false;
            Moor3 = false;
        }
        else if (bootBlueprint != null && bootBlueprint.isUnlocked && bootBlueprint.abilityGame == AbilityNames.MoorGame)
        {
            FarbeWechseln(BootImage, Color.yellow);
            Cross3 = false;
            Kong3 = false;
            Hoop3 = false;
            Moor3= true;
        }
        else
        {
            FarbeWechseln(BootImage, Color.white);
            Kong3 = false;
            Hoop3= false;
            Cross3= false;
            Moor3 = false;
        }

        UpdateAbility();
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
        else if (Hoop1 && Hoop2 && Hoop3)
        {
            PlayerPrefs.SetInt("AbilityNumber", 3);
            Debug.Log("HoopAbility is true");
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

/// <summary>
/// Select the type of game this item supports with an ability
/// </summary>
public enum AbilityNames
{
    None,
    HoopGame,
    CrossGame,
    KongGame,
    MoorGame
}
