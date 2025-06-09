using Cinemachine;
using System.Collections;
using UnityEngine;

public class TaschenBus : MonoBehaviour
{
    //kamera referenz für das wackeln, damit der soieler weiß, wenn man eine tasche/skateboard getroffen hat
    private CinemachineVirtualCamera huhnCam;
    private CinemachineBasicMultiChannelPerlin lsd;


    private void Start()
    {
        huhnCam = GameObject.FindWithTag("PlayerCam").GetComponent<CinemachineVirtualCamera>();
        lsd = huhnCam!.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Shaker());
        RunnerGamerOver.instance.Stolpern();
    }

    //kamera soll wackeln 
    IEnumerator Shaker()
    {
        lsd.m_AmplitudeGain = 2;
        yield return new WaitForSecondsRealtime(0.5f);
        lsd.m_AmplitudeGain = 0;
    }
}
