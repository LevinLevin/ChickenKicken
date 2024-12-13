using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tastaturopen : MonoBehaviour
{
    TouchScreenKeyboard Tastatur;

    // Start is called before the first frame update
    public void OpenKeyboard()
    {
        Tastatur = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}
