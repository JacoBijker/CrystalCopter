using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject CrystalType;
    public Material[] AlternateMaterials;

    private GameObject Crystal;
    private IGeneration generator = new Midpoint(200, 25);

    const float maxCrystalRotation = 0.25f;
    float z = 1;
    float terrainAnchor = -20;

    void SetupTerrain(float[] heights)
    {
        MeshFilter meshFilter = GetComponent(typeof(MeshFilter)) as MeshFilter;
        var mesh = new Mesh();
        meshFilter.mesh = mesh;

        ArrayList meshPoints = new ArrayList();
        ArrayList trianglePoints = new ArrayList();
        ArrayList normals = new ArrayList();
        ArrayList uvs = new ArrayList();
        for (int x = 0; x < heights.Length - 1; x++)
        {
            meshPoints.Add(new Vector3(x, terrainAnchor, z));
            meshPoints.Add(new Vector3(x + 1, terrainAnchor, z));
            if (terrainAnchor < 0)
            {
                meshPoints.Add(new Vector3(x, heights[x], z));
                meshPoints.Add(new Vector3(x + 1, heights[x + 1], z));
            }

            trianglePoints.Add(x * 4 + 0);
            trianglePoints.Add(x * 4 + 2);
            trianglePoints.Add(x * 4 + 1);
            trianglePoints.Add(x * 4 + 2);
            trianglePoints.Add(x * 4 + 3);
            trianglePoints.Add(x * 4 + 1);

            for (int i = 0; i < 4; i++)
                normals.Add(-Vector3.forward);

            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(1, 1));
        }

        mesh.vertices = meshPoints.ToArray(typeof(Vector3)) as Vector3[];
        mesh.triangles = trianglePoints.ToArray(typeof(int)) as int[];
        mesh.normals = normals.ToArray(typeof(Vector3)) as Vector3[];
        mesh.uv = uvs.ToArray(typeof(Vector2)) as Vector2[];
    }

    void SetupCollider(float[] heights)
    {
        EdgeCollider2D edgeCollider = GetComponent(typeof(EdgeCollider2D)) as EdgeCollider2D;

        ArrayList collisionPoints = new ArrayList();
        for (int x = 0; x < heights.Length - 1; x++)
            collisionPoints.Add(new Vector2(x, heights[x]));

        edgeCollider.points = collisionPoints.ToArray(typeof(Vector2)) as Vector2[];
    }

    private float lastHeight;
    public float GetLastHeight()
    {
        return lastHeight;
    }

    public void Initialize(TerrainInitializationInfo terrainInitInfo)
    {
        generator.SetStartHeight(terrainInitInfo.YStartPosition, terrainInitInfo.Range);
        this.terrainAnchor = terrainInitInfo.AnchorPoint;
        float[] midPointHeights = generator.Generate();
        lastHeight = midPointHeights[midPointHeights.Length - 1];
        SetupTerrain(midPointHeights);
        SetupCollider(midPointHeights);
        SetupCrystals(midPointHeights);

        MeshRenderer meshRenderer = GetComponent(typeof(MeshRenderer)) as MeshRenderer;
        meshRenderer.material = AlternateMaterials[terrainInitInfo.AlternateMaterialIndex];
    }

    private void SetupCrystals(float[] heights)
    {
        var index = Random.Range(0, heights.Length);
        var height = heights[index] + 1.75f;
        var position = new Vector3(index, height, z);

        Crystal = Instantiate(CrystalType, Vector3.zero, Quaternion.identity) as GameObject;
        Crystal.transform.parent = this.transform;
        Crystal.transform.localPosition = position;
        Quaternion currentRotation = Crystal.transform.rotation;
        currentRotation.z = Random.Range(-maxCrystalRotation, maxCrystalRotation);
        currentRotation.y = -currentRotation.y;

        var inverseMultiplier = this.transform.localScale.y < 0 ? -1 : 1;
        var scale = Crystal.transform.localScale;
        scale.y = scale.y * inverseMultiplier;
        Crystal.transform.localScale = scale;
        Crystal.transform.rotation = currentRotation;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
