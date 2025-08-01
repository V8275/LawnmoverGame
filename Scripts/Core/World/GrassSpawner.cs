using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToPlace;
    [SerializeField]
    private MeshFilter meshFilter;
    [SerializeField]
    private int numberOfObjects = 10;
    [SerializeField]
    private float yOffset = 0.5f;

    void Start()
    {
        PlaceObjectsOnMesh();
    }

    private void PlaceObjectsOnMesh()
    {
        if (meshFilter == null || objectToPlace == null)
        {
            Debug.LogError("MeshFilter or ObjectToPlace not assigned!");
            return;
        }

        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        for (int i = 0; i < numberOfObjects; i++)
        {
            int randomTriangleIndex = Random.Range(0, triangles.Length / 3) * 3;

            Vector3 v1 = vertices[triangles[randomTriangleIndex]];
            Vector3 v2 = vertices[triangles[randomTriangleIndex + 1]];
            Vector3 v3 = vertices[triangles[randomTriangleIndex + 2]];

            Vector3 randomPoint = GetRandomPointOnTriangle(v1, v2, v3);

            Vector3 worldPosition = meshFilter.transform.TransformPoint(randomPoint);

            worldPosition.y += yOffset;

            Instantiate(objectToPlace, worldPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPointOnTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        float r1 = Random.value;
        float r2 = Random.value;

        if (r1 + r2 > 1)
        {
            r1 = 1 - r1;
            r2 = 1 - r2;
        }

        return v1 + (v2 - v1) * r1 + (v3 - v1) * r2;
    }
}
