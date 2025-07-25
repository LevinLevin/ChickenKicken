using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;

public class Join : MonoBehaviour
{
    public TMP_Text[] names;

    public TMP_Text[] scores;

    public TMP_Text[] hoopScores;

    public TMP_Text[] kongScores;

    public TMP_Text[] crossScores;

    public TMP_Text[] moorScores;

    public TMP_Text playerRankText;

    private void Start()
    {
        UploadEntry();
    }

    private void LoadScoreHoopEntries()
    {
        Leaderboards.Score_Hoop.GetEntries(entries =>
        {
            foreach (var t in names)
                t.text = "";

            var length = Mathf.Min(names.Length, entries.Length);
            for (int i = 0; i < length; i++)
            {
                names[i].text = entries[i].Username.ToString();
                scores[i].text = entries[i].Score.ToString();
                hoopScores[i].text = entries[i].Extra;
            }
        });
    }

    private void LoadKongEntries()
    {
        Leaderboards.Kong.GetEntries(entries =>
        {
            foreach (var t in kongScores)
                t.text = "";

            var length = Mathf.Min(names.Length, entries.Length);
            for (int i = 0; i < length; i++)
            {
                kongScores[i].text = entries[i].Extra;
                crossScores[i].text = entries[i].Username;
            }
        });
    }

    private void LoadMoorEntries()
    {
        Leaderboards.Moor.GetEntries(entries =>
        {
            foreach (var t in moorScores)
                t.text = "";

            var length = Mathf.Min(names.Length, entries.Length);
            for (int i = 0; i < length; i++)
            {
                moorScores[i].text = entries[i].Username;
            }
        });
    }

    public void UploadEntry()
    {
        Leaderboards.Score_Hoop.UploadNewEntry(PlayerPrefs.GetString("NameDesHuhn",""), PlayerPrefs.GetInt("AnzahlDerPunkte",0), PlayerPrefs.GetInt("HighestCombo", 0).ToString(), isSuccessful =>
        {
            if (isSuccessful)
            {
                LoadScoreHoopEntries();
                ShowPlayerRank();
            }
        });
        Leaderboards.Kong.UploadNewEntry(PlayerPrefs.GetInt("AnzahlDerMeter", 0).ToString(), PlayerPrefs.GetInt("AnzahlDerPunkte", 0), PlayerPrefs.GetInt("HighscoreFZ", 0).ToString(), isSuccessful =>
        {
            if (isSuccessful)
                LoadKongEntries();
        });
        Leaderboards.Moor.UploadNewEntry(PlayerPrefs.GetInt("HighestLevel", 0).ToString(), PlayerPrefs.GetInt("AnzahlDerPunkte", 0),  isSuccessful =>
        {
            if (isSuccessful)
                LoadMoorEntries();
        });
    }

    private void ShowPlayerRank()
    {
        Leaderboards.Score_Hoop.GetPersonalEntry(OnPersonalEntryLoaded, OnError);
    }
    private void OnPersonalEntryLoaded(Entry playerEntry)
    {
        if (playerEntry.Rank > 0)
        {
            string rankSuffix = playerEntry.RankSuffix(); // e.g., "1st", "2nd"
            playerRankText.text = $"Your Rank: {rankSuffix}\nScore: {playerEntry.Score}";
            playerRankText.color = Color.green;
        }
        else
        {
            playerRankText.text = "You are not ranked yet.";
            playerRankText.color = Color.white;
        }
    }
    private void OnError(string error)
    {
        Debug.LogError($"Error fetching personal entry: {error}");
        playerRankText.text = "Could not load your rank.";
        playerRankText.color = Color.red;
    }
}
