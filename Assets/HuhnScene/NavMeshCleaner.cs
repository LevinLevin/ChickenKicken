using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class NavMeshCleaner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NavMesh.RemoveAllNavMeshData();
    }
}
