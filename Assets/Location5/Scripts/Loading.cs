using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{

    public GameObject PauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenuUI.SetActive(true);

        Invoke(nameof(resume), 3f);
    }

    void resume()
    {
        PauseMenuUI.SetActive(false);
    }
}
