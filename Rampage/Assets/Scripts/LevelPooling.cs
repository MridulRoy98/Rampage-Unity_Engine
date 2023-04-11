using System;
using UnityEngine;
using Random = UnityEngine.Random;
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

    
    private void Start()
    {
        //Subscribe to the event published by CameraManager
        myCameraManager = myCamera.GetComponent<CameraManager>();
        myCameraManager.OnTriggerPoint += DestroyLevel;

        //Spawn details
        spawnedLevels = new GameObject[3];
        initialPosition = new Vector3(0, 0, 96);
        spawnPosition = initialPosition;
        offsetAmount = -48;
        offsetMultiplier = 1;
        spawnCount = 0;

        foreach (var levelPrefab in levelPrefabs)
        {
            CreateNewLevel();
        }
    }

    private void CreateNewLevel()
    {
        GameObject newLevel = Instantiate(levelPrefabs[Random.Range(0, levelPrefabs.Length)], spawnPosition, Quaternion.identity);
        spawnPosition = new Vector3(0, 0, initialPosition.z + offsetAmount * offsetMultiplier);
        offsetMultiplier++;

        newLevel.transform.SetParent(levelParent);

        spawnedLevels[spawnCount] = newLevel;
        spawnCount++;

        
    }

    //Called when event triggered by Camera
    private void DestroyLevel(object sender, CameraManager.OnTriggerPointEventArgs e)
    {
        Debug.Log("Triggered");
        RemoveFromArray(e.triggerCount - 1);

        Instantiate(levelPrefabs[Random.Range(0, levelPrefabs.Length)], spawnPosition, Quaternion.identity);
        spawnPosition = new Vector3(0, 0, initialPosition.z + offsetAmount * offsetMultiplier);
        offsetMultiplier++;


    }
    private void RemoveFromArray(int index)
    {
        Destroy(spawnedLevels[index]);
    }


}
