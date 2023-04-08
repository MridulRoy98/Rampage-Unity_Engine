using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            //Spawn prefabs in scene maintaining offset
            GameObject level = Instantiate(levelPrefab, spawnPosition, Quaternion.identity);
            spawnPosition = new Vector3(0, 0, initialPosition.z + offsetAmount * chunkCounter);
            chunkCounter++;

            //Add the instantiated prefabs to another array to destroy later
            spawnedLevels[spawnCount] = level;
            spawnCount++;

            //Making the levels child of another GameObject
            level.transform.SetParent(levelParent);
        }
    }

    private void DestroyLevel(object sender, CameraManager.OnTriggerPointEventArgs e)
    {
        Debug.Log("Triggered");
        Destroy(spawnedLevels[e.numberOfTrigger - 1]);
    }
}
