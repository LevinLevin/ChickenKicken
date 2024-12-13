using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;

public class ShooterLife : MonoBehaviour
{
    //das spiel muss auch irgendwann enden
    public GameObject GameOverCanvas;
    public GameObject Canvas;
    int getroffen;

    //um den score zu präsentieren
    [Header("Highscore")]
    public TMP_Text txtLevel;
    public TMP_Text txtHighscore;
    public GameObject LevelText;
    int score;
    int highscore;

    //kamera, damit sie wackelt, wenn man getroffen wurde
    [Header("Kamera")]
    public CinemachineVirtualCamera huhnCam;
    private CinemachineBasicMultiChannelPerlin lsd;


    private void Start()
    {
        lsd = huhnCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        Canvas.SetActive(true);
        GameOverCanvas.SetActive(false);

        getroffen = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //checkt nur für die Bullet, um probleme zu verhindern
        if (other.CompareTag("Bullet"))
        {
            getroffen++;

            StartCoroutine(CameraShake());

            if(getroffen == 3)
            {
                GameOverCanvas.SetActive(true);
                Canvas.SetActive(false);

                //fps werden gesenkt für wenn das Spiel nicht so viel braucht
                Application.targetFrameRate = 25;

                //Highscore
                LeanTween.scale(LevelText, new Vector3(1.04f, 1.04f, 1.04f), 1f).setEaseOutQuart().setLoopPingPong();

                score = PlayerPrefs.GetInt("Level", 0);
                txtLevel.text = "Level: " + score.ToString();
                highscore = PlayerPrefs.GetInt("HighestLevel", 0);
                txtHighscore.text = "Highscore: " + highscore.ToString();
            }
        }
        else if(other.CompareTag("Barrel"))
        {
            StartCoroutine(CameraShake());

            GameOverCanvas.SetActive(true);
            Canvas.SetActive(false);

            //Highscore
            LeanTween.scale(LevelText, new Vector3(1.04f, 1.04f, 1.04f), 1f).setEaseOutQuart().setLoopPingPong();

            score = PlayerPrefs.GetInt("Level", 0);
            txtLevel.text = "Level: " + score.ToString();
            highscore = PlayerPrefs.GetInt("HighestLevel", 0);
            txtHighscore.text = "Highscore: " + highscore.ToString();
        }
    }

    public void Restart()
    {
        //Die Szene wird neu geladen
        SceneManager.LoadScene("Location5.1");
        PlayerPrefs.SetInt("Level", 0);
    }

    IEnumerator CameraShake()
    {
        lsd.m_AmplitudeGain = 2;
        yield return new WaitForSecondsRealtime(0.3f);
        lsd.m_AmplitudeGain = 0;
    }
}
