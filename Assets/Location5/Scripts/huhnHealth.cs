using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class huhnHealth : MonoBehaviour
{

    public GameObject herz1, herz2, herz3; //drei herzen indikator für leben

    public GameObject deathScreen; //gameoverscreen wird gezeigt wenn keine leben mehr vorhanden sind
    public GameObject LoadingScreen;

    int leben = 1;

    private void Start()
    {
        deathScreen.SetActive(false);

        leben = 1;
    }

    private void Update()
    {
        switch (leben)
        {
            case 1:
                herz1.SetActive(true);
                herz2.SetActive(true);
                herz3.SetActive(true);
                break;
            case 2:
                herz1.SetActive(true);
                herz2.SetActive(true);
                herz3.SetActive(false);
                break;
            case 3:
                herz1.SetActive(true);
                herz2.SetActive(false);
                herz3.SetActive(false);
                break;
            case 4:
                herz1.SetActive(false);
                herz2.SetActive(false);
                herz3.SetActive(false);

                deathScreen.SetActive(true);

                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        leben++;
    }

    #region RestartButton

    public void Restart()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Location5");

        LoadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            yield return null;
        }
    }
    
    //code von Solo Game Dev "Unity-Ladebildschirm - Einfaches Tutorial(2022)"
    #endregion
}
