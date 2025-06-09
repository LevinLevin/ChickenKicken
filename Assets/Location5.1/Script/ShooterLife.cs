using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShooterLife : MonoBehaviour
{
    //das spiel muss auch irgendwann enden
    public GameObject GameOverCanvas;
    public GameObject Canvas;
    int leben;
    [SerializeField] Image lifeFill;
    [SerializeField] GameObject lifeSlider;

    public Image bloodImage;

    //um den score zu präsentieren
    [Header("Highscore")]
    public TMP_Text txtLevel;
    public TMP_Text txtHighscore;
    int score;
    int highscore;

    //kamera, damit sie wackelt, wenn man getroffen wurde
    [Header("Kamera")]
    public CinemachineVirtualCamera huhnCam;
    private CinemachineBasicMultiChannelPerlin lsd;


    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 0;

        lsd = huhnCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        Canvas.SetActive(true);
        GameOverCanvas.SetActive(false);

        leben = 3;

        UpdateLifeUI();
        bloodImage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //checkt nur für die Bullet, um probleme zu verhindern
        if (other.CompareTag("Bullet"))
        {
            leben --;
            UpdateLifeUI();

            StartCoroutine(CameraShake());

            if(leben == 1)
            {
                bloodImage.gameObject.SetActive(true);
            }

            if (leben <= 0)
            {
                GameOver();
            }
        }
        else if(other.CompareTag("Barrel"))
        {
            GameOver();
        }
    }

    private void UpdateLifeUI()
    {
        float fill = Mathf.Clamp01((float)leben / 3);
        lifeFill.fillAmount = fill;
        LeanTween.scale(lifeSlider, Vector3.one * 1.05f, 0.2f)
         .setEaseOutBack()
         .setOnComplete(() =>
         {
             LeanTween.scale(lifeSlider, Vector3.one, 0.2f)
                      .setEaseInBack();
         });
    }

    void GameOver()
    {
        GameOverCanvas.SetActive(true);
        Canvas.SetActive(false);

        //fps werden gesenkt für wenn das Spiel nicht so viel braucht
        Application.targetFrameRate = 25;

        //Highscore
        LeanTween.scale(txtLevel.gameObject, new Vector3(1.04f, 1.04f, 1.04f), 1f).setEaseOutQuart().setLoopPingPong();

        score = PlayerPrefs.GetInt("Level", 0);
        txtLevel.text = "Level: " + score.ToString();
        highscore = PlayerPrefs.GetInt("HighestLevel", 0);
        txtHighscore.text = "Highest Level: " + highscore.ToString();
    }

    /// <summary>
    /// restarts the whole scene
    /// </summary>
    public void Restart()
    {
        //Die Szene wird neu geladen
        SceneManager.LoadScene("Location5.1");
        PlayerPrefs.SetInt("Level", 0);
    }

    /// <summary>
    /// Restores the health of the chicken
    /// </summary>
    public void SetLeben()
    {
        leben = 3;
        bloodImage.gameObject.SetActive(false);
        UpdateLifeUI();
    }

    IEnumerator CameraShake()
    {
        lsd.m_AmplitudeGain = 2;
        yield return new WaitForSecondsRealtime(0.3f);
        lsd.m_AmplitudeGain = 0;
    }
}
