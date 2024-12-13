using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    ScoreManager sm;

    public int currentLocation = 0;
    public GameObject[] locations;

    public LocationBlueprint[] platz;
    public Button erwerben;
    public Button start;
    public GameObject BtnStart;
    public Text kaufen;
    public Button futter;

    public GameObject LoadingScreen;
    public Text loadingStatus;

    // Start is called before the first frame update
    void Start()
    {
        sm= FindObjectOfType<ScoreManager>();

        foreach(LocationBlueprint loc in platz)
        {
            if(loc.price == 0)
            {
                loc.isUnlocked = true;
            }
            else
            {
                loc.isUnlocked = PlayerPrefs.GetInt(loc.name, 0)== 0 ? false: true;
            }
        }

        currentLocation = PlayerPrefs.GetInt("Selected", 0);
        foreach (GameObject location in locations)
        {
            location.SetActive(false);
        }

        locations[currentLocation].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }

    public void nAnderes()
    {
        locations[currentLocation].SetActive(false);

        currentLocation++;
        if (currentLocation == locations.Length)
        {
            currentLocation = 0;
        }

        locations[currentLocation].SetActive(true);
        LeanTween.scale(locations[currentLocation], new Vector3(1.05f, 1.05f, 1.05f), 0.5f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(locations[currentLocation], new Vector3(1f, 1f, 1f), 0.7f).setDelay(0.3f).setEase(LeanTweenType.easeOutBack);

        LeanTween.scale(BtnStart, new Vector3(1.01f, 1.01f, 1.01f), 0.5f).setDelay(0.1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(BtnStart, new Vector3(1f, 1f, 1f), 0.7f).setDelay(0.3f).setEase(LeanTweenType.easeOutBack);

        LocationBlueprint c = platz[currentLocation];
        if(!c.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("Selected", currentLocation);
    }

    public void Unlock()
    {
        LocationBlueprint c = platz[currentLocation];

        if(c.isPlayable) 
        {
            LeanTween.scale(BtnStart, new Vector3(1.04f, 1.04f, 1.04f), 0.5f).setEase(LeanTweenType.easeOutBack);
            LeanTween.scale(BtnStart, new Vector3(1f, 1f, 1f), 0.7f).setDelay(0.3f).setEase(LeanTweenType.easeOutBounce);

            PlayerPrefs.SetInt(c.name, 1);
            PlayerPrefs.SetInt("Selected", currentLocation);
            c.isUnlocked = true;

            if (sm != null)
            {
                sm.RemovePoint(c.price);
            }
            PlayerPrefs.SetInt("AnzahlDerPunkte", PlayerPrefs.GetInt("AnzahlDerPunkte", 0) - c.price);
        }
    }

    #region Play
    public void Play()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("Selected", 0) + 3);

        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("Selected", 0) + 3);

        LoadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp(operation.progress * 100f / 0.9f, 0f, 100f);

            loadingStatus.text = "Loading " + progressValue.ToString() + "%";

            yield return null;
        }
    }
    #endregion

    private void updateUI()
    {
        LocationBlueprint c = platz[currentLocation];
        if (c.isUnlocked)
        {
            erwerben.gameObject.SetActive(false);
            start.gameObject.SetActive(true);
        }
        else
        {
            erwerben.gameObject.SetActive(true);
            start.gameObject.SetActive(false);
            kaufen.text = "PURCHASE " + c.price;
            if (c.price < PlayerPrefs.GetInt("AnzahlDerPunkte", 0))
            {
                erwerben.interactable = true;
            }
            else
            {
                erwerben.interactable = false;
            }
        }
    }

    public void FutterKaufen()
    {
        PlayerPrefs.SetInt("AnzahlDesFutters", +15);
    }
}
