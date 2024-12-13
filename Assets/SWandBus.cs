using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SWandBus : MonoBehaviour
{
    //kamera referenz
    private CinemachineVirtualCamera huhnCam;

    private void Start()
    {
        //huhnCam = FindObjectOfType<CinemachineVirtualCamera>();
        huhnCam = GameObject.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Shaker());
    }

    IEnumerator Shaker()
    {
        huhnCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 2;
        yield return new WaitForSecondsRealtime(0.5f);
        huhnCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}
