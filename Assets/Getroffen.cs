using UnityEngine;

public class Getroffen : MonoBehaviour, IPooledObject
{
    //damit das Flugzeug nur von Patronen des Huhn getroffen werden kann
    public LayerMask PatroneL;

    //für die sounds
    private AudioSource explosionSound;

    //für die particles
    public ParticleSystem Particle;
    public bool once = true;

    //für die GameOver Canvas
    bool getroffen = false;
    bool einmal = true;

    bool isMusicOn;

    ScoreManager sm;

    public void OnObjectSpawn()
    {
        getroffen = false;
        einmal = true;
    }

    private void Start()
    {
        sm = FindObjectOfType<ScoreManager>();

        if (PlayerPrefs.GetInt("MusikAus", 1) == 0)
        {
            explosionSound.volume = 0;
        }


        explosionSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (getroffen == true && einmal == true)
        {
            GameOverManager.Instance.AddFlugzeug();
            getroffen = false;
            einmal = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PatroneL == (PatroneL | (1 << other.gameObject.layer)))
        {
            //der Treffer wird gezählt
            getroffen = true;
            Kaboom();
            //der sound wird gespielt 
            explosionSound.Play();

            //punkte werden addiert
            //PlayerPrefs.SetInt("AnzahlDerPunkte", PlayerPrefs.GetInt("AnzahlDerPunkte", 0) + 33);
            if (sm != null)
            {
                sm.AddPoint(67);
            }
        }
    }

    public void Kaboom()
    {
        var em = Particle.emission;

        em.enabled = true;
        Particle.Play();

        once = false;
    }
}
