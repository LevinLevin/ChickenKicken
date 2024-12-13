using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;

public class ShooterSchafe : MonoBehaviour
{
    public GameObject sheep1;
    Transform sheep1Transform = null;
    Vector3 target1Position = Vector3.zero;


    [SerializeField] GameObject chicken;
    ShooterMovement movement; //um die aktuelle position des spielers zu ermitteln
    private int Lane;
    public int MyLane; //the lane of the sheep
    private float time; //time the sheep is hiding or show


    public GameObject bulletPrefab;
    Rigidbody bulletRb;
    public float bulletSpeed;
    [SerializeField] private GameObject gun;

    private bool versteckt;
    private bool shooten;
    private bool flying;

    //this bool checks if it is time for the endboss
    public bool ready;
    public bool schonPassiert;
    public int stufe;
    //endboss
    [SerializeField] ShooterKuh Kuh;

    int schaden;


    //punkte bekommen
    public GameObject txtPunkte;
    ScoreManager sM;


    // Start is called before the first frame update
    void Start()
    {
        sM = FindObjectOfType<ScoreManager>();

        bulletRb = bulletPrefab.GetComponent<Rigidbody>();

        movement= chicken.GetComponent<ShooterMovement>();

        Kuh.GetComponent<ShooterKuh>();

        sheep1Transform = sheep1.transform;

        Verstecken();

        //stufe wird erhöht, wenn getroffen
        stufe = 0;
        schonPassiert= false;
        ready= false;

        if(PlayerPrefs.GetInt("AbilityNumber", 0) == 4)
        {
            schaden = 7;
            Debug.Log("Schaden auf 10 minimiert");
        }
        else
        {
            schaden = 12;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(movement != null)
        {
            Lane = movement.desiredLane;
            Debug.Log("Lane: " + Lane);
        }

        if(Lane== MyLane && !shooten && !versteckt)
        {
            ShootBullet();
            shooten= true;
        }
        else if(Lane !=MyLane)
        {
            shooten= false;
        }

        //schaden muss mit Stufe erreicht werden, um kuh zu beschwören
        if(stufe == schaden)
        {
            ready= true;
            if(!schonPassiert)
            {
                Kuh.Setready(1);
                schonPassiert= true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (sM != null)
            {
                LeanTween.scale(txtPunkte, new Vector3(1.08f, 1.08f, 1.08f), 0.6f).setEase(LeanTweenType.easeOutBounce);
                LeanTween.scale(txtPunkte, new Vector3(1f, 1f, 1f), 0.3f).setDelay(0.3f).setEase(LeanTweenType.easeOutBack);
                sM.AddPoint(1);
                stufe++;
            }
        }
    }

    private void Verstecken()
    {
        if(!versteckt)
        {
            // Set the target position to move the sheep down by 5 units on the Y axis
            target1Position.x = sheep1Transform.position.x;
            target1Position.y = sheep1Transform.position.y - 5f;
            target1Position.z = sheep1Transform.position.z;

            // Move the sheep to the target position
            sheep1Transform.position = target1Position;

            versteckt= true;
            shooten = false;
        }
        time = Random.Range(3.0f, 5.0f);
        Invoke(nameof(hervorkommen), time);
    }

    public void hervorkommen()
    {
        if(versteckt && !ready)
        {
            // Set the target position to move the sheep up by 5 units on the Y axis
            target1Position.x = sheep1Transform.position.x;
            target1Position.y = sheep1Transform.position.y + 5f;
            target1Position.z = sheep1Transform.position.z;

            // Move the sheep to the target position
            sheep1Transform.position = target1Position;

            versteckt = false;
        }
        time = Random.Range(2.0f, 4.0f);
        Invoke(nameof(Verstecken), time);
    }

    public void ShootBullet()
    {
        // bullet erstellen
        GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
        Destroy(bullet, 4f);

        // Add force to the bullet
        bullet.GetComponent<Rigidbody>().AddForce(-transform.right * bulletSpeed, ForceMode.Impulse);
    }
}
