using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    //um die Flugzeuge anzuzeigen, die man getroffen hat
    public Text FZgetroffen;
    public GameObject FZGetroffen;

    public Text Highscore;
    public static int highscore;
    private int vorherHighscore;

    //um die canvas an und auszuschalten
    public GameObject Buttons;

    //um das huhn zu deaktivieren
    public GameObject huhn;

    public TMP_Text txtCurrentFZ;
    private int scoreFZ;

    //schaltet die GameOver Canvas an 
    public GameObject GameOverCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
            Instance = this;

        highscore = PlayerPrefs.GetInt("HighscoreFZ", 0);
    }

    private void Start()
    {
        //Eine Animation für den text, der im GameOver Screen die getroffenen Flugzeuge anzeigt
        LeanTween.scale(FZGetroffen, new Vector3(1.04f, 1.04f, 1.04f), 1f).setEaseOutQuart().setLoopPingPong();

        txtCurrentFZ.text = scoreFZ.ToString() + " Turboprops";
        //die andere canvas wird ausgeschaltet
        Buttons.SetActive(true);

        //fps werden gesenkt für wenn das Spiel nicht so viel braucht
        Application.targetFrameRate = 0;
    }

    // Update is called once per frame
    public void EndGame()
    {
        Application.targetFrameRate = 25;

        Buttons.SetActive(false);
        huhn.SetActive(false);

        //die gespeicherte anzahl der abgeschossenen Flugzeuge wird in den Text eingefügt
        FZgetroffen.text = "Hit Turboprops: " + scoreFZ;
        /*der vorher highscore ist das selbe wie die anzahl der flugzeuge und wird später als vergleichswert genutzt
         * sollte der vorher highscore jetzt groesser sein als der highscore, wird der heighscore durch den vorher
         * highscore ersetzt
         */
        vorherHighscore = scoreFZ;
        if (vorherHighscore > PlayerPrefs.GetInt("HighscoreFZ", 0))
        {
            PlayerPrefs.SetInt("HighscoreFZ", vorherHighscore);
            highscore = PlayerPrefs.GetInt("HighscoreFZ", 0);
        }
        //der highscore wird angezeigt
        Highscore.text = "Highscore: " + highscore.ToString();
        GameOverCanvas.SetActive(true);

    }

    public void Restart()
    {
        //DIe Anzahl der Flugzeuge wird zurückgesetzt
        scoreFZ = 0;
        //Die Szene wird neu geladen
        SceneManager.LoadScene("Location3.1");
    }

    public void AddFlugzeug()
    {
        scoreFZ++;
        txtCurrentFZ.text = scoreFZ.ToString() + " Turboprops";
    }
}
