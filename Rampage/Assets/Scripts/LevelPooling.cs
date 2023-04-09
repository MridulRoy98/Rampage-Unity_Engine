using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class LevelPooling : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 spawnPosition;
    private int offsetAmount;
    private int chunkCounter;
    private int spawnCount;

    // Main Camera object and Class
    [SerializeField] private Camera myCamera;
    CameraManager myCameraManager;

    [Header ("Level Prefabs and Triggers")]
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private Transform levelParent;
    [SerializeField] private GameObject[] spawnedLevels;

    
    private void Start()
    {
        //Subscribe to the event published by CameraManager
        myCameraManager = myCamera.GetComponent<CameraManager>();
        myCameraManager.OnTriggerPoint += DestroyLevel;

        //Spawn details
        spawnedLevels = new GameObject[levelPrefabs.Length];
        initialPosition = new Vector3(0, 0, 96);
        spawnPosition = initialPosition;
        offsetAmount = -48;
        chunkCounter = 1;
        spawnCount = 0;

        foreach (var levelPrefab in levelPrefabs)
        {
            CreateNewLevel();
        }
    }

    private void CreateNewLevel()
    {
        GameObject newLevel = Instantiate(levelPrefabs[Random.Range(0, levelPrefabs.Length)], spawnPosition, Quaternion.identity);
        spawnPosition = new Vector3(0, 0, initialPosition.z + offsetAmount * chunkCounter);
        chunkCounter++;
        spawnCount++;

        newLevel.transform.SetParent(levelParent);
        AddToArray(newLevel);
    }

    //Called when event triggered by Camera
    private void DestroyLevel(object sender, CameraManager.OnTriggerPointEventArgs e)
    {
        Debug.Log("Triggered");
        RemoveFromArray(e.numberOfTrigger);
    }

    private void AddToArray(GameObject newLevel)
    {
        spawnedLevels[spawnCount] = newLevel;
    }
    private void RemoveFromArray(int index)
    {
        Destroy(spawnedLevels[index - 1]);
        CreateNewLevel();
        myCameraManager.OnTriggerPoint -= DestroyLevel;
    }


}
