using UnityEngine;
using UnityEngine.UI;

public class ShooterShoot : MonoBehaviour
{
    private int munition;

    //joystick kontrolliert die richtung, des wurfgeschoss
    [SerializeField] private DynamicJoystick joystick;

    [SerializeField] private Transform gun;

    ObjectPooler objectPooler;//f�r die bullets

    public GameObject bulletPrefab;
    public float bulletSpeed = 20;
    //public float bulletAngle = 5f;

    Vector3 bulletDirectionRight;
    Vector3 bulletDirectionLeft;

    [SerializeField] Image ammoFill;
    [SerializeField] GameObject ammoSlider;

    private void Start()
    {
        bulletDirectionRight = (gun.forward + gun.right * 0.9f + gun.up * 0.6f).normalized;
        bulletDirectionLeft = (gun.forward + -gun.right * 0.9f + gun.up * 0.6f).normalized;

        objectPooler = ObjectPooler.Instance;

        munition = 10;
        UpdateAmmoUI();
    }

    private void OnEnable()
    {
        Munition.munitionCollected += SetMunition;
    }

    //private void Update()
    //{
    //    float horizontalInput = joystick.Horizontal;
    //    float verticalInput = joystick.Vertical;

    //    Vector3 joystickDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
    //    Vector3 objectPosition = gun.transform.position;
    //    Vector3 objectToJoystick = objectPosition + joystickDirection - objectPosition;

    //    bulletDirection = Quaternion.AngleAxis(bulletAngle, Vector3.right) * objectToJoystick;
    //}

    public void FireBulletRight()
    {
        if (munition <= 0) return;
        GameObject bullet = objectPooler.SpawnFromPool("Bullet", gun.position, Quaternion.identity);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = bulletDirectionRight * bulletSpeed;
        munition--;
        UpdateAmmoUI();
    }

    public void FireBulletLeft()
    {
        if (munition <= 0) return;
        GameObject bullet = objectPooler.SpawnFromPool("Bullet", gun.position, Quaternion.identity);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = bulletDirectionLeft * bulletSpeed;
        munition--;
        UpdateAmmoUI();
    }

    //public void Fire()
    //{
    //    if (munition <= 0) return;
    //    if ((joystick.Horizontal != 0 || joystick.Vertical != 0) && munition != 0)
    //    {
    //        FireBullet(bulletDirection);
    //        munition--;
    //    }
    //}

    /// <summary>
    /// Sets the eggs of the chicken +7 and is capped over 30
    /// </summary>
    public void SetMunition()
    {
        if (munition > 30)
            return;

        munition += 7;
        UpdateAmmoUI();
    }

    /// <summary>
    /// Refills the eggs of the chicken to 20
    /// </summary>
    public void RefillMunition()
    {
        munition += 20;
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        float fill = Mathf.Clamp01((float)munition / 30);
        ammoFill.fillAmount = fill;
        LeanTween.scale(ammoSlider, Vector3.one * 1.05f, 0.2f)
         .setEaseOutBack()
         .setOnComplete(() =>
         {
             LeanTween.scale(ammoSlider, Vector3.one, 0.2f)
                      .setEaseInBack();
         });
    }

    private void OnDisable()
    {
        Munition.munitionCollected -= SetMunition;
    }
}
