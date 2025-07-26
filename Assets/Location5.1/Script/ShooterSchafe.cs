using UnityEngine;

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

    private ObjectPooler pooler;
    private ScoreManager sM;

    public GameObject bulletPrefab;
    Rigidbody bulletRb;
    [Tooltip("Something around 5 to 15")] public float bulletSpeed;
    [SerializeField] private Transform gun;

    private bool versteckt;
    private bool shooten;
    private bool flying;

    //this bool checks if it is time for the endboss
    private bool ready;
    private bool schonPassiert;

    //endboss
    [SerializeField] ShooterKuh Kuh;

    [SerializeField] GameObject DeathStone;

    int schafLeben;

    //punkte bekommen
    public GameObject txtPunkte;

    void Start()
    {
        pooler = ObjectPooler.Instance;

        sM = ScoreManager.Instance;

        bulletRb = bulletPrefab.GetComponent<Rigidbody>();

        movement= chicken.GetComponent<ShooterMovement>();

        Kuh.GetComponent<ShooterKuh>();

        sheep1Transform = sheep1.transform;

        Verstecken();

        schonPassiert= false;
        ready= false;

        if(PlayerPrefs.GetInt("AbilityNumber", 0) == 4)
        {
            schafLeben = 7;
        }
        else
        {
            schafLeben = 12;
        }
    }

    void Update()
    {
        if(movement != null)
        {
            Lane = movement.desiredLane;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (sM != null)
            {
                LeanTween.scale(txtPunkte, new Vector3(1.08f, 1.08f, 1.08f), 0.6f).setEase(LeanTweenType.easeOutBounce);
                LeanTween.scale(txtPunkte, new Vector3(1f, 1f, 1f), 0.3f).setDelay(0.3f).setEase(LeanTweenType.easeOutBack);
                sM.AddPoint(57);
                schafLeben--;
                //schaden muss mit Stufe erreicht werden, um kuh zu beschw�ren
                if (schafLeben <= 0)
                {
                    ready = true;
                    if (!schonPassiert)
                    {
                        Kuh.Setready(1);
                        schonPassiert = true;
                    }
                }
            }
        }
    }

    private void Verstecken()
    {
        if(!versteckt)
        {
            if(ready)
                DeathStone.SetActive(true);

            // Set the target position to move the sheep down by 5 units on the Y axis
            target1Position.x = sheep1Transform.position.x;
            target1Position.y = sheep1Transform.position.y - 5f;
            target1Position.z = sheep1Transform.position.z;

            // Move the sheep to the target position
            sheep1Transform.position = target1Position;

            versteckt= true;
            shooten = false;
        }
        time = Random.Range(3.0f, 6.0f);
        Invoke(nameof(hervorkommen), time);
    }

    public void hervorkommen()
    {
        if(versteckt && !ready)
        {
            DeathStone.SetActive(false);
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

        GameObject bullet = pooler.SpawnFromPool("Apple", gun.position, Quaternion.identity);

        // Add force to the bullet
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        bulletRB.linearVelocity = Vector3.zero;
        bulletRB.AddForce(-gun.right * bulletSpeed, ForceMode.Impulse);
    }

    public void ResetStats()
    {
        if (PlayerPrefs.GetInt("AbilityNumber", 0) == 4)
        {
            schafLeben = 7;
        }
        else
        {
            schafLeben = 12;
        }

        schonPassiert = false;
        ready = false;
    }
}
