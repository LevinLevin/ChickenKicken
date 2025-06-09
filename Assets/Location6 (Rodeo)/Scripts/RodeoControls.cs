using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RodeoControls : MonoBehaviour
{
    //bool springt;
    int Cowboyhut;

    public Slider slider;
    public float speed = 1.0f;
    private int pinCount = 0; //count it for the different attack
    private bool isMovingRight = true;



    // Use this for initialization
    void Start()
    {
        Cowboyhut = PlayerPrefs.GetInt("SelectedHut");
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePoints();
        UpdateSlider();
    }

    public void Springen()
    {
        //springt = true;
        if (Cowboyhut == 2)
        {
            Invoke(nameof(Springtnicht), 1.7f);
        }
        else
        {
            Debug.Log("hüpf");
            Invoke(nameof(Springtnicht), 1f);
        }

        //if you press springen when ult is not done FEHLSCHLAG
    }

    public void Springtnicht()
    {
        //springt = false;
    }


    private void UpdatePoints()
    {

        if (slider.value >= 0.95f)
        {
            Debug.Log("Slider value is greater than or equal to 0.95!");
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // get the first touch
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Point +1 lol");
                }
            }
            else
            {
                Debug.Log("GameOver");
            }
        }

        if(slider.value <= 0.2f)
        {
            Debug.Log("Slider value is smaller than or equal to 0.2!");
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // get the first touch
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Point +1 hehe");
                }
            }
            else
            {
                Debug.Log("GameOver");
            }
        }

        //if slider is not below 0.2 or greater than 0.95 and Input.Touchcount - FEHLSCHLAG
    }

    public void UpdateSlider()
    {
        // Check if slider handle is at left or right end
        if (slider.value == slider.minValue)
        {
            isMovingRight = true;
        }
        else if (slider.value == slider.maxValue)
        {
            isMovingRight = false;
        }

        // Move slider handle left or right based on current direction
        if (isMovingRight)
        {
            slider.value += Time.deltaTime * speed;
        }
        else
        {
            slider.value -= Time.deltaTime * speed;
        }

        // Check if slider handle has been pinned from left to right
        if (isMovingRight && slider.value == slider.maxValue)
        {
            Debug.Log(pinCount);
            pinCount++;
            if (pinCount == 3)
            {
                pinCount = 0;
                // Reset slider position to left end
                slider.value = slider.minValue;
            }
        }
    }
}