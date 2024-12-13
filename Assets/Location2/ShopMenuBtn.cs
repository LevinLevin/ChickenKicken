using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopMenuBtn : MonoBehaviour
{
    public void ZurückInsMenü()
    {
        //Geht eine Scene zurück 
        SceneManager.LoadScene("Shop");
        Time.timeScale = 1f;
    }
}
