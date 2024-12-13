using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //um die Flugzeuge anzuzeigen, die man getroffen hat
    public Text FZgetroffen;
    public GameObject FZGetroffen;
    public static int anzahl;

    public Text Highscore;
    public static int highscore;
    private int vorherHighscore;

    //um die canvas an und auszuschalten
    public GameObject Buttons;

    //um das huhn zu deaktivieren
    public GameObject huhn;

    // Start is called before the first frame update
    void Awake()
    {
        anzahl = PlayerPrefs.GetInt("AnzahlDerFZ", 0);
        highscore = PlayerPrefs.GetInt("HighscoreFZ", 0);
    }

    private void Start()
    {
        FZgetroffen.text = "Hit Turboprops: " +anzahl.ToString();
        //Eine Animation für den text, der im GameOver Screen die getroffenen Flugzeuge anzeigt
        LeanTween.scale(FZGetroffen, new Vector3(1.04f, 1.04f, 1.04f), 1f).setEaseOutQuart().setLoopPingPong();

        Highscore.text = "Highscore: " + highscore.ToString();
        //die andere canvas wird ausgeschaltet
        Buttons.SetActive(false);
        //das huhn wird deaktiviert 
        huhn.SetActive(false);

        //fps werden gesenkt für wenn das Spiel nicht so viel braucht
        Application.targetFrameRate = 25;
    }

    // Update is called once per frame
    void Update()
    {
        //die gespeicherte anzahl der abgeschossenen Flugzeuge wird in den Text eingefügt
        FZgetroffen.text = "Hit Turboprops: " + PlayerPrefs.GetInt("AnzahlDerFZ", 0);
        /*der vorher highscore ist das selbe wie die anzahl der flugzeuge und wird später als vergleichswert genutzt
         * sollte der vorher highscore jetzt groesser sein als der highscore, wird der heighscore durch den vorher
         * highscore ersetzt
         */
        vorherHighscore = PlayerPrefs.GetInt("AnzahlDerFZ", 0);
        if (vorherHighscore > PlayerPrefs.GetInt("HighscoreFZ", 0))
        {
            PlayerPrefs.SetInt("HighscoreFZ", vorherHighscore);
            highscore = PlayerPrefs.GetInt("HighscoreFZ", 0);
        }
        //der highscore wird angezeigt
        Highscore.text = "Highscore: " + highscore.ToString();
    }

    public void Restart()
    {
        //DIe Anzahl der Flugzeuge wird zurückgesetzt
        PlayerPrefs.SetInt("AnzahlDerFZ", 0);
        //Die Szene wird neu geladen
        SceneManager.LoadScene("Location3.1");
    }

    public void OnDestroy()
    {
        PlayerPrefs.SetInt("AnzahlDerFZ", 0);
    }
}
