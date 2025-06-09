using UnityEngine;

public class WandBus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        RunnerGamerOver.instance.EndGame();
    }
}
