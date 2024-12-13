using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterShoot : MonoBehaviour
{
    //joystick kontrolliert die richtung, des wurfgeschoss
    [SerializeField] private DynamicJoystick joystick;

    [SerializeField] private GameObject gun;

    ObjectPooler objectPooler;//für die bullets

    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float bulletAngle = 5f;

    Vector3 bulletDirection;

    private void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 joystickDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 objectPosition = gun.transform.position;
        Vector3 objectToJoystick = objectPosition + joystickDirection - objectPosition;

        bulletDirection = Quaternion.AngleAxis(bulletAngle, Vector3.right) * objectToJoystick;

        Debug.DrawRay(objectPosition, objectToJoystick, Color.red);
    }

    private void FireBullet(Vector3 direction)
    {
        // Instantiate the bullet prefab at the position of the GameObject
        GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
        Destroy(bullet,10f);

        // Apply the modified direction vector as the initial velocity of the bullet
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = direction.normalized * bulletSpeed;
    }

    public void Fire()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            FireBullet(bulletDirection);
        }
    }
}
