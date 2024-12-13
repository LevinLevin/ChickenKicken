using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameHuhn : MonoBehaviour
{
    public Text HuhnName;
    // Start is called before the first frame update
    void Update()
    {
        HuhnName.text = PlayerPrefs.GetString("NameDesHuhn");
    }
}
