using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
