using UnityEngine;
using Random = UnityEngine.Random;
using Unity.AI.Navigation;

public class LevelPooling : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 spawnPosition;
    private int offsetAmount;
    private int offsetMultiplier;
    private int spawnCount;

    // Main Camera object and Class
    [SerializeField] private Camera myCamera;
    CameraManager myCameraManager;

    [Header ("Level Prefabs and Triggers")]
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private Transform levelParent;
    [SerializeField] private GameObject[] spawnedLevels;
    [SerializeField] private NavMeshSurface surface;

    private void Start()
    {
        //Subscribe to the event published by CameraManager
        myCameraManager = myCamera.GetComponent<CameraManager>();
        myCameraManager.OnTriggerPoint += LevelRegenSystem;

        //Spawn details
        spawnedLevels = new GameObject[10];
        initialPosition = new Vector3(0, 0, 96);
        spawnPosition = initialPosition;
        offsetAmount = -48;
        offsetMultiplier = 1;
        spawnCount = 0;

        foreach (var levelPrefab in levelPrefabs)
        {
            CreateNewLevel();
        }
        surface.BuildNavMesh();
    }


    //Creating level in the front
    private void CreateNewLevel()
    {
        GameObject newLevel = Instantiate(levelPrefabs[Random.Range(0, levelPrefabs.Length)], spawnPosition, Quaternion.identity);
        spawnPosition = new Vector3(0, 0, initialPosition.z + offsetAmount * offsetMultiplier);
        offsetMultiplier++;

        newLevel.transform.SetParent(levelParent);

        spawnedLevels[spawnCount] = newLevel;
        spawnCount++;
    }

    //Deleting the level in the back
    private void DestroyOldLevel(int index)
    {
        Destroy(spawnedLevels[index]);
    }


    //Called when event triggered by Camera
    private void LevelRegenSystem(object sender, CameraManager.OnTriggerPointEventArgs e)
    {
        DestroyOldLevel(e.triggerCount-1);
        CreateNewLevel();
    }
}
