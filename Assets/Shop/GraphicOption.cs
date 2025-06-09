using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicOption : MonoBehaviour
{
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
        Debug.Log("Quality level set to: " + QualitySettings.names[qualityIndex]);
    }
}
