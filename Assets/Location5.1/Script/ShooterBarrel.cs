using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBarrel : MonoBehaviour
{
    [SerializeField] private ParticleSystem PSDust;

    private void OnEnable()
    {
        PSDust.Play();
    }
}
