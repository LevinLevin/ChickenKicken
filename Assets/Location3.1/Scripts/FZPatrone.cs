using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FZPatrone : MonoBehaviour
{
    float speed = 10;

    bool isExplode = false;

    //für die particles
    public ParticleSystem Particle;
    public bool once = true;

    //für die explosionskraft
    [SerializeField] private Rigidbody rb;
    public bool ready = false;
    [SerializeField] private int flugkurve = 1;

    //für den schaden 
    private float radius = 2.5f;

    //sound
    public AudioSource smallExplosion;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        smallExplosion = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isExplode == false)
        {
            transform.Translate(Vector3.forward * speed / 2 * Time.deltaTime);
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (isExplode == true && ready == false)
        {
            //sound
            if (PlayerPrefs.GetInt("MusikAus", 1) == 1)
            {
                smallExplosion.Play();
            }
            //schaden wird zum Huhn hinzugefügt
            Schaden();
            //die bombe wird weg katapultiert durch rigidbody force
            rb.AddForce(new Vector3(0, flugkurve , 0), ForceMode.Impulse);
            //ready bool, damit es nur einmal passiert und nicht jedes frame
            ready = true;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        Kaboom();
        isExplode = true;
    }

    //Explosion
    public void Kaboom()
    {

        //particles
        var em = Particle.emission;
        //var dur = Particle.duration;

        em.enabled = true;
        Particle.Play();
        //Particle.Stop();
        //em.enabled = false;

        once = false;
    }

    void Schaden()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            SchadenNehmen sn =nearbyObject.GetComponent<SchadenNehmen>();
            if(sn != null)
            {
                sn.Autsch();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(transform.position, radius);
    }

}
