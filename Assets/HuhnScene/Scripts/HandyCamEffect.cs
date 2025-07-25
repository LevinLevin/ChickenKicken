using UnityEngine;

/// <summary>
/// Apply on camera for a handy recorded feel
/// </summary>
public class HandyCamEffect : MonoBehaviour
{
    public float shakeIntensity = 0.1f;   // Stärke des Zitterns
    public float shakeFrequency = 1.0f;   // Frequenz des Zitterns

    private Vector3 initialPosition;
    private float shakeTimer;

    void Start()
    {
        initialPosition = transform.localPosition;
        shakeTimer = Random.Range(0f, 100f); // Damit es nicht synchron bei allen gleich aussieht
    }

    void Update()
    {
        shakeTimer += Time.deltaTime * shakeFrequency;

        float offsetX = Mathf.PerlinNoise(shakeTimer, 0f) - 0.5f;
        float offsetY = Mathf.PerlinNoise(0f, shakeTimer) - 0.5f;

        Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0f) * shakeIntensity;
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + shakeOffset, Time.deltaTime * 5f);
    }
}
