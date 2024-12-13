using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusik");
        if(musicObj.Length> 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 4)
        {
            gameObject.GetComponent<AudioSource>().Pause();
        }
        else if (SceneManager.GetActiveScene().buildIndex <=4 && !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            if(PlayerPrefs.GetInt("MusikAus", 1) == 1)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                gameObject.GetComponent<AudioSource>().Pause();
            }
        }
    }
}
