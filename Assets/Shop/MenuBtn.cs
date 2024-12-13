using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBtn : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Text loadingStatus;

    public void ZurückInsMenü()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Huhn");

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp(operation.progress * 100f / 0.9f, 0f, 100f);

            loadingStatus.text = "Loading " + progressValue.ToString() + "%";

            yield return null;
        }
    }
}
