using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RunnerGamerOver : MonoBehaviour
{

    public static RunnerGamerOver instance;

    //für die cameras
    [Header("Cameras")]
    [SerializeField] CinemachineVirtualCamera BusCam;
    [SerializeField] CinemachineVirtualCamera HuhnCam;

    //um die treffer zu zählen
    private int gestolpert;
    //um die trefer auf ihre ability zu werten
    private int leben;
    //um die meter anzuzeigen
    [Header("Texts")]
    private int gelaufen;
    private int hs;
    public Text txtMeter;
    public GameObject TextMeter;
    public Text txtHighscore;
    //um die andere canvas auszuschalten
    [Header("Canvas")]
    public GameObject andereCanvas;
    public GameObject gameoverCanvas;
    public Image ImgBlood;

    //das huhn muss zerstört werden
    public GameObject huhn;

    //für die Musik
    public GameObject musikObjekt;
    float duration = 3f;
    float targetVolume = 0.4f;
    float endVolume = 0f;

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
        LeanTween.scale(TextMeter, new Vector3(1.04f, 1.04f, 1.04f), 1f).setEaseOutQuart().setLoopPingPong();

        //am anfang sollen die fehltritte 0 sein
        gestolpert = 0;

        //die camera wird erst für gewisse sekunden auf den bus und dann auf das huhn gerichtet
        StartCoroutine(StartSequenz());

        //nach jeder sekunde wird ein meter gezählt solange man weniger als 3 mal gestolpert ist
        StartCoroutine(Metermachen());

        //die musik sollte langsam lauter werden am anfang des spiels
        StartCoroutine(FadeMusicIn());

        //testen wie viele leben man hat
        if(PlayerPrefs.GetInt("AbilityNumber", 0) == 2)
        {
            leben = 6;
            //test the result
            Debug.Log("Leben:" + leben);
        }
        else
        {
            leben = 3;
            Debug.Log("Leben:" + leben);
        }

        ImgBlood.gameObject.SetActive(false);
    }

    public void Stolpern()
    {
        gestolpert++;
        if(gestolpert == leben -1) //das vorletzte leben triggert das Blut
        {
            ImgBlood.gameObject.SetActive(true);
        }

        if (gestolpert >= leben)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        //canvas muessen aktiviert und deaktiviert werden
        andereCanvas.SetActive(false);
        gameoverCanvas.SetActive(true);

        //das huhn verschwindet
        huhn.SetActive(false);

        //fps werden gesenkt für wenn das Spiel nicht so viel braucht
        Application.targetFrameRate = 25;

        //die Musik faded aus
        StartCoroutine(FadeMusicOut());

        //der highscore wird gesetzt

        txtMeter.text = "Distance: " + gelaufen.ToString();
        hs = PlayerPrefs.GetInt("AnzahlDerMeter", 0);
        txtHighscore.text = "Highscore: " + hs.ToString();
        if (gelaufen > PlayerPrefs.GetInt("AnzahlDerMeter", 0))
        {
            PlayerPrefs.SetInt("AnzahlDerMeter", gelaufen);
        }
    }

    //fuer den nochmal button
    public void Nochmal()
    {
        SceneManager.LoadScene("Location4");
    }

    IEnumerator Metermachen()
    {
        while(gestolpert < 3)
        {
            yield return new WaitForSeconds(1);
            gelaufen++;
        }
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
        float start = musikObjekt.GetComponent<AudioSource>().volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musikObjekt.GetComponent<AudioSource>().volume = Mathf.Lerp(start, endVolume, currentTime / duration);
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
