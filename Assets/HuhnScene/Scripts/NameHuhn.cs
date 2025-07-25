using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameHuhn : MonoBehaviour
{
    public Text HuhnName;

    public Button BtnLeaderboard;

    // Start is called before the first frame update
    void Start()
    {
        HuhnName.text = PlayerPrefs.GetString("NameDesHuhn");
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("NameDesHuhn"))){
            BtnLeaderboard.interactable = true;
        }
    }

    public void UpdateName()
    {
        HuhnName.text = PlayerPrefs.GetString("NameDesHuhn");
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("NameDesHuhn")))
        {
            BtnLeaderboard.interactable = true;
        }
    }
}
