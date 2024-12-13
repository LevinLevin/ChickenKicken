using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public void ZurückInsMenü()
    {
        //Geht eine Scene zurück 
        SceneManager.LoadScene("MainMenu");
    }
}
