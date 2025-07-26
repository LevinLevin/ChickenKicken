using UnityEngine;
using UnityEngine.UI;

public class SchadenNehmen : MonoBehaviour
{
    public static SchadenNehmen instance;

    //macht nen sound
    private AudioSource huhnSound;

    //for life indicator
    public Image ImgBlood;

    //leben
    private int leben;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        ImgBlood.gameObject.SetActive(false);

        huhnSound = GetComponent<AudioSource>();

        if(PlayerPrefs.GetInt("AbilityNumber", 0) == 1)
        {
            leben = 6;
            Debug.Log("Mehr leben dank ability");
        }
        else
        {
            leben = 3;
        }
    }

    public void Autsch()
    {
        huhnSound.Play();
        leben --;

        if (leben <= 0)
        {
            GameOverManager.Instance.EndGame();
            ImgBlood.gameObject.SetActive(false);
            return;
        }

        //wenn leben nur noch 2 sind, kommt blut auf screen
        if (leben == 2)
        {
            ImgBlood.gameObject.SetActive(true);
            Color color = ImgBlood.color;
            color.a = 0.4f;
            ImgBlood.color = color;
        }
        else if (leben <= 1)
        {
            Color color = ImgBlood.color;
            color.a = 0.8f;
            ImgBlood.color = color;
        }
    }
}
