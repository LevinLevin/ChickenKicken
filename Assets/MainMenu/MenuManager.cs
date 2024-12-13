using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    //this script is for all the buttons and fuctions in the main menu

    public Text TxtUpdate;
    public GameObject upText;

    public TMP_Dropdown qualityDropdown;

    public GameObject HuhnFoto;

    void Start()
    {
        LoadQuality();

        //für den update text
        TxtUpdate.text = PlayerPrefs.GetString("NameDesHuhn") + " likes you :)";
        LeanTween.scale(upText, new Vector3(1.04f, 1.04f, 1.04f), 1f).setEaseOutQuart().setLoopPingPong();
    }

    //shows the scene where the informations is in
    public void showInformations()
    {
        SceneManager.LoadScene("Informtions");
    }


    //delete data button
    public void deleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    #region Resolution

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
        Debug.Log("Quality level set to: " + QualitySettings.names[qualityIndex]);
    }

    public void LoadQuality()
    {
        int qualityIndex = PlayerPrefs.GetInt("quality", 2);
        SetQuality(qualityIndex);
        qualityDropdown.value = qualityIndex;
    }

    #endregion

    #region Framerate
    /*
    public void SetFramerate(int framerateIndex)
    {

        switch (framerateIndex)
        {
            case 0:
                Application.targetFrameRate = 0;
                Debug.Log("Framerate = nolimit");
                PlayerPrefs.SetInt("Framerate", Application.targetFrameRate);
                break;
            case 1:
                Application.targetFrameRate = 30;
                Debug.Log("Framerate = 30");
                PlayerPrefs.SetInt("Framerate", Application.targetFrameRate);
                break;
            case 2:
                Application.targetFrameRate = 60;
                Debug.Log("Framerate = 60");
                PlayerPrefs.SetInt("Framerate", Application.targetFrameRate);
                break;
            case 3:
                Application.targetFrameRate = 120;
                Debug.Log("Framerate = 120");
                PlayerPrefs.SetInt("Framerate", Application.targetFrameRate);
                break;
            default:
                Debug.LogWarning("Invalid framerate limit selected!");
                break;
        }
    }

    */

    #endregion

    public void showSettings()
    {
        LeanTween.moveLocalY(HuhnFoto, -245, 1.3f);
    }

    public void closeSettings()
    {
        LeanTween.moveLocalY(HuhnFoto, -1000, 0f);
    }
}
