using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LEaderboard : MonoBehaviour
{
    public void OpenLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
