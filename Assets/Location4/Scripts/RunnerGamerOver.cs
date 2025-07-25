using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RunnerGamerOver : MonoBehaviour
{

    public static RunnerGamerOver instance;

    //für die cameras
    [Header("Cameras")]
    [SerializeField] CinemachineVirtualCamera BusCam;
    [SerializeField] CinemachineVirtualCamera HuhnCam;
    //um die treffer auf ihre ability zu werten
    private int leben;
    //um die meter anzuzeigen
    [Header("Texts")]
    public Text txtMeter;
    public GameObject TextEndMeter;
    public Text txtHighscore;
    public TMP_Text txtCurrentMeter;
    public TMP_Text txtCurrentHighscore;

    //um die andere canvas auszuschalten
    [Header("Canvas")]
    public GameObject andereCanvas;
    public GameObject gameoverCanvas;
    public Image ImgBlood;

    //das huhn muss zerstört werden
    public GameObject huhn;

    //für die Musik
    [Header("Music")]
    public AudioSource musikObjekt;
    float duration = 3f;
    float targetVolume = 0.4f;
    float endVolume = 0f;

    [Header("Speed")]
    public float currentSpeed = 10f;
    float distance;
    public float growthRate = 0.1f;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Eine Animation für den text, der im GameOver Screen die Meter anzeigt
        LeanTween.scale(TextEndMeter, new Vector3(1.04f, 1.04f, 1.04f), 1f).setEaseOutQuart().setLoopPingPong();

        //die camera wird erst für gewisse sekunden auf den bus und dann auf das huhn gerichtet
        StartCoroutine(StartSequenz());

        //die musik sollte langsam lauter werden am anfang des spiels
        StartCoroutine(FadeMusicIn());

        //testen wie viele leben man hat
        if(PlayerPrefs.GetInt("AbilityNumber", 0) == 2)
        {
            leben = 6;
        }
        else
        {
            leben = 3;
        }

        txtCurrentHighscore.text = PlayerPrefs.GetInt("AnzahlDerMeter", 0).ToString() + " m";
        ImgBlood.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(leben > 0)
        {
            distance += currentSpeed * Mathf.Exp(growthRate * Time.time) * Time.deltaTime;
            txtCurrentMeter.text = Mathf.FloorToInt(distance).ToString() + " m";
        }
    }

    public void Stolpern()
    {
        leben--;
        if(leben == 2) //das vorletzte leben triggert das Blut
        {
            ImgBlood.gameObject.SetActive(true);
            Color color = ImgBlood.color;
            color.a = 0.4f;
            ImgBlood.color = color;
        }
        else if(leben == 1)
        {
            Color color = ImgBlood.color;
            color.a = 0.8f;
            ImgBlood.color = color;
        }

        if (leben <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        //canvas muessen aktiviert und deaktiviert werden
        andereCanvas.SetActive(false);
        gameoverCanvas.SetActive(true);
        txtCurrentMeter.gameObject.SetActive(false);

        //das huhn verschwindet
        huhn.SetActive(false);

        //fps werden gesenkt für wenn das Spiel nicht so viel braucht
        Application.targetFrameRate = 25;

        //die Musik faded aus
        StartCoroutine(FadeMusicOut());

        //der highscore wird gesetzt

        txtMeter.text = "Distance: " + Mathf.RoundToInt(distance).ToString() + " m";

        int hs = PlayerPrefs.GetInt("AnzahlDerMeter", 0);

        if ((int)distance > hs)
        {
            txtHighscore.text = "Highscore: " + Mathf.RoundToInt(distance).ToString() + " m";
            PlayerPrefs.SetInt("AnzahlDerMeter", Mathf.RoundToInt(distance));
        }
        else
        {
            txtHighscore.text = "Highscore: " + Mathf.RoundToInt(hs).ToString() + " m";
        }
    }

    //fuer den nochmal button
    public void Nochmal()
    {
        SceneManager.LoadScene("Location4");
    }

    IEnumerator StartSequenz()
    {
        //das huhn wird erst nach waitforseconds sekunden vorspann gezeigt
        yield return new WaitForSecondsRealtime(5f);
        CameraSwitcher.SwitchCamera(HuhnCam);
    }

    IEnumerator FadeMusicIn()
    {
        float currentTime = 0;
        float start = musikObjekt.GetComponent<AudioSource>().volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musikObjekt.GetComponent<AudioSource>().volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    IEnumerator FadeMusicOut()
    {
        float currentTime = 0;
        float start = musikObjekt.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musikObjekt.volume = Mathf.Lerp(start, endVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    private void OnEnable()
    {
        CameraSwitcher.Register(BusCam);
        CameraSwitcher.Register(HuhnCam);
        CameraSwitcher.SwitchCamera(BusCam);
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(BusCam);
        CameraSwitcher.Unregister(HuhnCam);
    }
}
