using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Join : MonoBehaviour
{


    [SerializeField]
    private List<TextMeshProUGUI> names;

    [SerializeField]
    private List<TextMeshProUGUI> scores;

    [Header("Huete der Huehner")]
    public GameObject[] hute1;
    public GameObject[] hute2;
    public GameObject[] hute3;

    public int[] huteIndex;

    private string publicLeaderboardKey = "cb1e15a144abd82a016662861f6d694bef1af31d2658bbfedb9389e59c03c921";

    string Username;
    int Score;
    string HutIndex;

    public void Start()
    {
        Username = PlayerPrefs.GetString("NameDesHuhn");
        Score = PlayerPrefs.GetInt("AnszahlDerPunkte", 0);
        HutIndex = PlayerPrefs.GetInt("SelectedHut").ToString();

        Upload(Username, Score, HutIndex);
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            DisplayHut();
            for (int i = 0; i < names.Count && i < msg.Length; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void Upload(string pUsername, int pScore, string pHutIndex)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, pUsername, pScore, pHutIndex, ((msg) =>
        {
            GetLeaderboard();
        }));
    }

    public void DisplayHut()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            Debug.Log(msg);
            //für jeden der ersten drei plätze wird der hutindex ermittelt und aus dem leaderboard übergeben
            for(int i = 0; i < msg.Length; i++)
            {
                //angenommen der erste platz (msg[0]) hätte den dritten hut hute1[3],
                //dann wird der erste string "3" als int an erster stelle des int array huteIndex[0] übergeben
                //huteIndex[0] übergibt dann den wert an einen lokalen int hutIndex1 also wieder 3
                //aus dem ersten array der hüte hute1, wird dann der Hut an 3. Stelle aktiviert
                huteIndex[i] = int.Parse(msg[i].Extra);
                Debug.Log("hut index " + huteIndex[i]);
            }

            //jedes huhn benötigt seinen eigenen Hutindex
            int hutIndex1;
            int hutIndex2;
            int hutIndex3;

            //der hut index wird dann auf den übergebenen String aus dem leaderboard gesetzt
            hutIndex1 = huteIndex[0];
            hutIndex2 = huteIndex[1];
            hutIndex3 = huteIndex[2];
            Debug.Log("hutIndex1: " + hutIndex1);

            //dann wird der hut index benutzt um im Gameobject array den richtigen hut zu aktivieren
            hute1[hutIndex1].SetActive(true);
            hute2[hutIndex2].SetActive(true);
            hute3[hutIndex3].SetActive(true);
        }));
    }
}
