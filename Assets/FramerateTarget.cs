using UnityEngine;

public class FramerateTarget : MonoBehaviour
{
    [Tooltip("0 Full Framerate, 1 60fps, 2 30fps, 3 20fps")]
    public int howMany;

    // Start is called before the first frame update
    void Start()
    {
        switch(howMany)
        {
            case 0: Application.targetFrameRate = Screen.currentResolution.refreshRate;
                break;
            case 1: Application.targetFrameRate = 60;
                break;
            case 2: Application.targetFrameRate= 30;
                break;
            case 3: Application.targetFrameRate = 20;
                break;
        }

        
    }
}
