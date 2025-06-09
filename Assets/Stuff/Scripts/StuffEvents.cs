using System;
using UnityEngine;

public class StuffEvents : MonoBehaviour
{
    public static event Action OnUIChange;

    public static void TriggerUIChange()
    {
        OnUIChange?.Invoke();
    }
}
