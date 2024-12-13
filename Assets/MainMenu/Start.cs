using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void Play()
    {
        //Load scene1 (sample scene)
        SceneManager.LoadScene("Shop");
    }

}
