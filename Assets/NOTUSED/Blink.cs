using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public GameObject upText;

    public void Start()
    {
        LeanTween.scale(upText, new Vector3(1.04f, 1.04f, 1.04f), 1f).setEaseOutQuart().setLoopPingPong();
        //LeanTween.scale(upText, new Vector3(1f, 1f, 1f), 0.7f).setDelay(0.3f).setEase(LeanTweenType.easeOutBounce);
    }
}
