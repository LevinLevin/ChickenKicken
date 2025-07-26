using UnityEngine;

public class Fliegen : MonoBehaviour, IPooledObject
{
    public float speed = 10.0f;
    private Rigidbody rb;

    float startFreezeTime;
    float stopFreezeTime = 2.0f;

    //sound
    public AudioSource propellerSound;

    public void Start()
    {

        if (PlayerPrefs.GetInt("MusikAus", 1) == 1)
        {
            propellerSound.Play();
        }

        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation= true;
    }

    public void OnObjectSpawn()
    {
        LeanTween.moveLocalY(gameObject, 7f, 8f).setEase(LeanTweenType.easeOutSine);

        startFreezeTime = Time.time;

        //StartCoroutine(warte());

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
        }
        else
        {
            Debug.Log("kein rigidbody");
        }

        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    void Update()
    {
        rb.linearVelocity = new Vector3(speed, 0, 0);

        if (rb != null && rb.freezeRotation == true)
        {
            float currentTime = Time.time;
            if (currentTime > startFreezeTime + stopFreezeTime)
            {
                rb.freezeRotation = false;
                if(rb.constraints == RigidbodyConstraints.FreezePosition)
                {
                    rb.constraints = RigidbodyConstraints.None;
                }
            }
        }

        if(transform.position.x > 45)
        {
            gameObject.SetActive(false);
        }
    }


    /*IEnumerator warte()
    {
        if (PlayerPrefs.GetInt("MusikAus", 1) == 1)
        {
            propellerSound.Play();
        }
        yield return new WaitForSecondsRealtime(13f);
        propellerSound.Stop();
        yield return new WaitForSecondsRealtime(2f);
        gameObject.SetActive(false);
    }*/
}
