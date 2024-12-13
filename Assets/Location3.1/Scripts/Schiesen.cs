using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schiesen : MonoBehaviour
{
    public GameObject BulletObject;

    bool isShooten;
    bool stopShooten;

    //für den schies sound
    private AudioSource popSound;
    [Range (0.1f, 0.5f)]
    public float pitchChangeMultiplier = 0.2f; //"Random Audio in Unity : Sound Design Tutorial" by Natty GameDev

    // Start is called before the first frame update
    void Start()
    {
        popSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isShooten)
        {
            ShootenFalse();
            //patrone wird an der stelle des huhns erstellt 
            GameObject bullet = Instantiate(BulletObject, transform.position, transform.rotation);
            //der sound wird gespielt 
            if(PlayerPrefs.GetInt("MusikAus", 1)== 1)
            {
                popSound.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
                popSound.Play();
            }
            //die Patrone wird wieder Zerstört nach ca 5 sek
            Destroy(bullet, 1f);
        }
    }

    // Update is called once per frame
    public void Shoot()
    {
        GameObject bullet = Instantiate(BulletObject, transform.position, transform.rotation);
        Destroy(bullet, 5f);
    }

    public void StartSchiesen()
    {
        stopShooten = false;
        Invoke("ShootenTrue", 0.2f);
    }

    public void StopSchiesen()
    {
        isShooten = false;
        stopShooten = true;
    }

    void ShootenFalse()
    {
        isShooten = false;
        if (stopShooten == false)
        {
            Invoke("ShootenTrue", 0.2f);
        }
    }

    void ShootenTrue()
    {
        isShooten = true;
    }
}
