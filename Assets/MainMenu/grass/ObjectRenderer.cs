using UnityEngine;

public class ObjectRenderer : MonoBehaviour
{
    public MeshRenderer targetRenderer; // The target mesh renderer to render the objects on
    public GameObject objectPrefab; // The prefab of the object to be rendered
    public int numObjects = 25; // Total number of objects to render
    public float placementRadius = 5f; // Maximum radius for random placement
    public bool randomRotation = true; // Enable random rotation
    public float yOffset = 0f; // Y offset to be added to the placement position

    private void Start()
    {
        Vector3 targetPosition = targetRenderer.transform.position;

        for (int i = 0; i < numObjects; i++)
        {
            // Generate random offsets for X and Z axes within the specified radius
            float randomX = Random.Range(-placementRadius, placementRadius);
            float randomZ = Random.Range(-placementRadius, placementRadius);

            // Create a new position by modifying only X and Z coordinates
            Vector3 position = new Vector3(targetPosition.x + randomX, targetPosition.y + yOffset, targetPosition.z + randomZ);

            // Create a new instance of the object prefab
            GameObject renderedObject = Instantiate(objectPrefab, position, Quaternion.identity);

            // Set the parent to the target mesh to make it move and rotate with it
            renderedObject.transform.parent = targetRenderer.transform;

            // Apply random rotation on the Y-axis if enabled
            if (randomRotation)
            {
                float randomRotationY = Random.Range(0f, 360f);
                renderedObject.transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);
            }
        }
    }
}
