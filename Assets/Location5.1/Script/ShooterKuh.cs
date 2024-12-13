using System.Collections;
using UnityEngine;

public class ShooterKuh : MonoBehaviour
{
    public bool ready;
    int readyCount;

    Transform kuhTransform = null;
    Vector3 targetPosition = Vector3.zero;

    [Header("Power")]
    public GameObject gun1, gun2, gun3;
    public GameObject KugelPref;
    public float bulletSpeed;

    //ein int für das random event
    int gunPicker;

    [Header("Alle Schafe")]
    public ShooterSchafe shooterSchaf1;
    public ShooterSchafe shooterSchaf2;
    public ShooterSchafe shooterSchaf3;
    public ShooterSchafe shooterSchaf4;

    int level;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;

        readyCount = 0;
        kuhTransform = transform;


        //Diese Zeile muss deaktiviert werden, wenn demo version raus ist
        //Endgame();
    }

    void Endgame()
    {
        // Set the target position to move the kuh up by 5 units on the Y axis
        targetPosition.x = kuhTransform.position.x;
        targetPosition.y = kuhTransform.position.y + 5f;
        targetPosition.z = kuhTransform.position.z;

        // Move the kuh to the target position
        kuhTransform.position = targetPosition;

        StartCoroutine(Schiessen());
    }

    void EndTheEndgame(int iLevel)
    {
        Debug.Log("Endgame ist beendet");
        //das level wird nach jeder runde erhöht um einen score zu berechnen
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

        //der bool muss zurück gesetzt werden, sodass alle schafe wieder auftauchen
        shooterSchaf1.ready = false;
        shooterSchaf2.ready = false;
        shooterSchaf3.ready = false;
        shooterSchaf4.ready = false;

        shooterSchaf1.schonPassiert= false;
        shooterSchaf2.schonPassiert = false;
        shooterSchaf3.schonPassiert = false;
        shooterSchaf4.schonPassiert = false;

        shooterSchaf1.stufe = 0;
        shooterSchaf2.stufe = 0;
        shooterSchaf3.stufe = 0;
        shooterSchaf4.stufe = 0;

        readyCount = 0;
    }

    public void Setready(int anzahl)
    {
        readyCount += anzahl;

        if (readyCount == 4)
        {
            Debug.Log("Jetzt kommt der Endboss");
            Endgame();
        }
    }

    public IEnumerator Schiessen()
    {
        yield return new WaitForSeconds(4f);

        Rollen();

        yield return new WaitForSeconds(3f);

        Rollen();

        yield return new WaitForSeconds(3f);

        Rollen();

        yield return new WaitForSeconds(3f);

        Rollen();

        yield return new WaitForSeconds(3f);

        Rollen();

        yield return new WaitForSeconds(3f);

        EndTheEndgame(1);
    }

    void Rollen()
    {
        gunPicker = Random.Range(1, 4);

        switch (gunPicker)
        {
            case 1:
                GameObject bullet = Instantiate(KugelPref, gun1.transform.position, Quaternion.identity);
                Destroy(bullet, 4f);
                bullet.GetComponent<Rigidbody>().AddForce(-transform.forward * bulletSpeed, ForceMode.Impulse);
                break;

                case 2:
                GameObject kugel = Instantiate(KugelPref, gun2.transform.position, Quaternion.identity);
                Destroy(kugel, 4f);
                kugel.GetComponent<Rigidbody>().AddForce(-transform.forward * bulletSpeed, ForceMode.Impulse);
                break;

                case 3:
                GameObject kullel = Instantiate(KugelPref, gun3.transform.position, Quaternion.identity);
                Destroy(kullel, 4f);
                kullel.GetComponent<Rigidbody>().AddForce(-transform.forward * bulletSpeed, ForceMode.Impulse);
                break;

        }
    }
}
