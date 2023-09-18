using UnityEngine;
using System.Collections;

public class TerrainController : MonoBehaviour
{
    public Transform mainCamera;
    public GameObject terrainType;

    private GameObject sectionOne;
    private GameObject sectionTwo;

    private GameObject lastSection;
    private int AlternateMaterialIndex = 0;

    private float DelayUpdate = 0;

    void Restart()
    {
        AlternateMaterialIndex = 0;
        lastSection = null;

        if (sectionOne != null)
            DestroyImmediate(sectionOne);

        if (sectionTwo != null)
            DestroyImmediate(sectionTwo);

        DelayUpdate = 1;
        Start();
    }

    private Transform GetComponent(GameObject section, string Name)
    {
        return section.transform.Find(Name);
    }

    private void CreateSection(ref GameObject section, float x)
    {
        float groundStartHeight = 0;
        float roofStartHeight = 0;
        float startPosition = 0;
        if (lastSection != null)
        {
            roofStartHeight = GetComponent(lastSection, "Roof").GetComponent<TerrainGenerator>().GetLastHeight();
            groundStartHeight = GetComponent(lastSection, "Ground").GetComponent<TerrainGenerator>().GetLastHeight();
            startPosition = GetX();

            DestroyImmediate(section);
        }

        GameObject newTerrain = Instantiate(terrainType, new Vector3(x, 0, 1), Quaternion.identity) as GameObject;
        var roof = GetComponent(newTerrain, "Roof");
        var ground = GetComponent(newTerrain, "Ground");

        newTerrain.SendMessage("Initialize", startPosition);
        roof.SendMessage("Initialize", new TerrainInitializationInfo(roofStartHeight, -20, 20, AlternateMaterialIndex));
        ground.SendMessage("Initialize", new TerrainInitializationInfo(groundStartHeight, -20, 20, AlternateMaterialIndex));

        section = newTerrain;
        lastSection = section;
    }

    private float GetX()
    {
        return lastSection.transform.position.x + GetComponent(lastSection, "Roof").GetComponent<MeshRenderer>().bounds.size.x;
    }

    void Start()
    {
        CreateSection(ref sectionOne, 0);
        CreateSection(ref sectionTwo, GetX());
    }

    void TerrainSwap(int index)
    {
        this.AlternateMaterialIndex = Mathf.Min(index-1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (DelayUpdate > 0)
        {
            DelayUpdate -= Time.deltaTime;
            return;
        }

        if (mainCamera.transform.position.x > lastSection.transform.position.x)
        {
            if (lastSection == sectionOne)
                CreateSection(ref sectionTwo, GetX());
            else
                CreateSection(ref sectionOne, GetX());
        }
    }
}
