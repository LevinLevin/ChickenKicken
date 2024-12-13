using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public Text scoreText;
    public static int score;


    private void Awake()
    {
        score = PlayerPrefs.GetInt("AnzahlDerPunkte", 0);
        scoreText.text = score.ToString() + " C-Bucks";
    }


    public void AddPoint(int pPunkte)
    {
        score += pPunkte;
        scoreText.text = score.ToString() + " C-Bucks";
    }

    public void RemovePoint(int pPunkte)
    {
        score -= pPunkte;
        scoreText.text = score.ToString() + " C-Bucks";
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("AnzahlDerPunkte", score);
    }

    //aufruf zum speichern für andere methoden
    public void LoadScore()
    {
        PlayerPrefs.SetInt("AnzahlDerPunkte", score);
    }


    //Lerp by Hamza Herbou "Unity lerp between 2 values, Mathf.Lerp" - (Deleted Code)
}
