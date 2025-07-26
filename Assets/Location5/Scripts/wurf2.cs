using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wurf2 : MonoBehaviour, IPooledObject
{
    Rigidbody rb;

    private GameObject punkteText;

    bool amLeben = true;
    bool amFliegen;

    float flugzeit;
    float fallzeit;

    public float direction = 6f;

    private int interval = 17;

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        amLeben = true;

        Fliegen();
        rb = gameObject.GetComponent<Rigidbody>();

        punkteText = GameObject.FindGameObjectWithTag("TxtPunkte");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % interval == 0)
        {
            flugzeit = Random.Range(0.7f, 1.5f);
        }
        else if (Time.frameCount % interval == 1)
        {
            fallzeit = Random.Range(0.7f, 1.5f);
        }

        if (amLeben && amFliegen == true)
        {
            rb.linearVelocity = new Vector3(direction, 3.5f);
        }
    }

    private void Fliegen()
    {
        amFliegen = true;
        Invoke(nameof(Stuerzen),flugzeit);
    }
    private void Stuerzen()
    {
        amFliegen = false;
        Invoke(nameof(Fliegen), fallzeit);
    }

    public void stirb()
    {
        if(amLeben)
        {
            amLeben = false;

            LeanTween.scale(punkteText, new Vector3(1.08f, 1.08f, 1.08f), 0.6f).setEase(LeanTweenType.easeOutBounce);
            PlayerPrefs.SetInt("AnzahlDerPunkte", PlayerPrefs.GetInt("AnzahlDerPunkte", 0) + 22343);
            LeanTween.scale(punkteText, new Vector3(1f, 1f, 1f), 0.3f).setDelay(0.3f).setEase(LeanTweenType.easeOutBack);

            gameObject.SetActive(false);
        }
    }
}
