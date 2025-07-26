using System.Collections;
using TMPro;
using UnityEngine;

public class ShooterKuh : MonoBehaviour
{
    public static ShooterKuh Instance;

    public bool ready;
    int readyCount;

    int preGun;

    Transform kuhTransform = null;
    Vector3 targetPosition = Vector3.zero;

    [Header("Power")]
    public Transform gun1, gun2, gun3;
    //public GameObject KugelPref;
    private ObjectPooler pooler;
    public float bulletSpeed;
    private float wurfSpeed;

    //ein int f�r das random event
    int gunPicker;

    [Header("Alle Schafe")]
    public ShooterSchafe shooterSchaf1;
    public ShooterSchafe shooterSchaf2;
    public ShooterSchafe shooterSchaf3;
    public ShooterSchafe shooterSchaf4;

    int level;

    [Header("ThemeMusic")]
    [SerializeField] AudioClip MainTheme;
    [SerializeField] AudioClip BossTheme;
    [SerializeField] AudioSource MusicPlayer;

    [Header("Upgrades")]
    [SerializeField] GameObject UpgradeWindow;
    [SerializeField] GameObject BtnAmmo;
    [SerializeField] GameObject BtnHealth;
    [SerializeField] TMP_Text TxtWave;
    [SerializeField] ShooterLife huhnLife;
    [SerializeField] ShooterShoot huhnShoot;

    [Header("Bubble")]
    [SerializeField] GameObject Bubble;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1f;

        if (PlayerPrefs.GetInt("MusikAus", 1) == 1)
        {
            MusicPlayer.clip = MainTheme;
            MusicPlayer.Play();
        }
        else
        {
            MusicPlayer.volume = 0.0f;
        }

        UpgradeWindow.SetActive(false);

        pooler = ObjectPooler.Instance;

        level = 0;
        wurfSpeed = 1f;

        readyCount = 0;
        kuhTransform = transform;

        Bubble.SetActive(false);

        //Diese Zeile muss deaktiviert werden, wenn demo version raus ist
        //Endgame();
        //ShowUpgradeWindow();
    }

    void Endgame()
    {
        MusicPlayer.Stop();
        MusicPlayer.clip = BossTheme;
        MusicPlayer.volume += 0.05f;
        MusicPlayer.Play();

        // Set the target position to move the kuh up by 5 units on the Y axis
        targetPosition.x = kuhTransform.position.x;
        targetPosition.y = kuhTransform.position.y + 5f;
        targetPosition.z = kuhTransform.position.z;

        // Move the kuh to the target position
        kuhTransform.position = targetPosition;

        Bubble.SetActive(true);
        LeanTween.scale(Bubble, new Vector2(1.02f, 1.02f), 0.5f).setLoopPingPong();

        //wurfspeed ist hoeher
        if(level == 2)
        {
            wurfSpeed = 0.9f;
        }
        else if(level == 3)
        {
            wurfSpeed = 0.8f;
        }
        else if(level >= 4)
        {
            wurfSpeed = 0.7f;
        }

        StartCoroutine(Schiessen());
    }

    void EndTheEndgame(int iLevel)
    {
        MusicPlayer.Stop();
        MusicPlayer.clip = MainTheme;
        MusicPlayer.volume -= 0.05f;
        MusicPlayer.Play();

        //das level wird nach jeder runde erh�ht um einen score zu berechnen
        level += iLevel;
        PlayerPrefs.SetInt("Level", level);
        if(level >= PlayerPrefs.GetInt("Level", 0))
        {
            PlayerPrefs.SetInt("HighestLevel", level);
        }

        // Set the target position to move the kuh up by 5 units down the Y axis
        targetPosition.x = kuhTransform.position.x;
        targetPosition.y = kuhTransform.position.y - 5f;
        targetPosition.z = kuhTransform.position.z;

        // Move the kuh to the target position
        kuhTransform.position = targetPosition;

        Bubble.SetActive(false);

        ShowUpgradeWindow();

        //schafe werden zurueckgesetzt
        shooterSchaf1.ResetStats();
        shooterSchaf2.ResetStats();
        shooterSchaf3.ResetStats();
        shooterSchaf4.ResetStats();

        readyCount = 0;
    }

    void ShowUpgradeWindow()
    {
        Time.timeScale = 0f;

        UpgradeWindow.SetActive(true);
        TxtWave.text = "Wave: " + level.ToString();
        LeanTween.scale(BtnAmmo, Vector3.one * 1.05f, 0.8f).setEaseInOutSine().setLoopPingPong().setIgnoreTimeScale(true);
        LeanTween.scale(BtnHealth, Vector3.one * 1.05f, 0.8f).setEaseInOutSine().setLoopPingPong().setIgnoreTimeScale(true);
    }

    public void SetAmmo()
    {
        Time.timeScale = 1f;
        huhnShoot.RefillMunition();
        UpgradeWindow.SetActive(false);
    }

    public void SetHealth()
    {
        Time.timeScale = 1f;
        huhnLife.SetLeben();
        UpgradeWindow.SetActive(false);
    }

    public void Setready(int anzahl)
    {
        readyCount += anzahl;

        if (readyCount == 4)
        {
            Endgame();
        }
    }

    public IEnumerator Schiessen()
    {
        yield return new WaitForSecondsRealtime(4f);

        Rollen();

        yield return new WaitForSecondsRealtime(wurfSpeed);

        Rollen();

        yield return new WaitForSecondsRealtime(wurfSpeed);

        Rollen();

        yield return new WaitForSecondsRealtime(wurfSpeed);

        Rollen();

        yield return new WaitForSecondsRealtime(wurfSpeed);

        Rollen();

        yield return new WaitForSecondsRealtime(wurfSpeed);

        Rollen();

        yield return new WaitForSecondsRealtime(wurfSpeed);

        Rollen();

        yield return new WaitForSecondsRealtime(wurfSpeed);

        Rollen();

        yield return new WaitForSecondsRealtime(wurfSpeed);

        Rollen();

        yield return new WaitForSecondsRealtime(wurfSpeed);

        Rollen();

        yield return new WaitForSecondsRealtime(3);

        EndTheEndgame(1);
    }

    void Rollen()
    {
        int newGun;

        do
        {
            newGun = Random.Range(1, 4);
        } while (newGun == preGun);

        gunPicker = newGun;
        preGun = newGun;

        switch (gunPicker)
        {
            case 1:
                //GameObject bullet = Instantiate(KugelPref, gun1.transform.position, Quaternion.identity);
                GameObject bullet = pooler.SpawnFromPool("Barrel", gun1.position, Quaternion.identity);

                //Destroy(bullet, 4f);
                //bullet.GetComponent<Rigidbody>().AddForce(-transform.forward * bulletSpeed, ForceMode.Impulse);
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
                bulletRB.linearVelocity = Vector3.zero;
                bulletRB.AddForce(-gun1.forward * bulletSpeed, ForceMode.Impulse);
                break;

                case 2:
                //GameObject kugel = Instantiate(KugelPref, gun2.transform.position, Quaternion.identity);
                GameObject burrel = pooler.SpawnFromPool("Barrel", gun2.position, Quaternion.identity);

                //Destroy(burrel, 4f);
                //burrel.GetComponent<Rigidbody>().AddForce(-transform.forward * bulletSpeed, ForceMode.Impulse);
                Rigidbody burrelRB = burrel.GetComponent<Rigidbody>();
                burrelRB.linearVelocity = Vector3.zero;
                burrelRB.AddForce(-gun2.forward * bulletSpeed, ForceMode.Impulse);
                break;

                case 3:
                //GameObject kullel = Instantiate(KugelPref, gun3.transform.position, Quaternion.identity);
                GameObject kullel = pooler.SpawnFromPool("Barrel", gun3.position, Quaternion.identity);

                //Destroy(kullel, 4f);
                //kullel.GetComponent<Rigidbody>().AddForce(-transform.forward * bulletSpeed, ForceMode.Impulse);
                Rigidbody kullelRB = kullel.GetComponent<Rigidbody>();
                kullelRB.linearVelocity = Vector3.zero;
                kullelRB.AddForce(-gun1.forward * bulletSpeed, ForceMode.Impulse);
                break;

        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
