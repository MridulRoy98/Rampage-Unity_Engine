using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPooling : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 spawnPosition;
    private int offsetAmount = -48;
    private int chunkCounter = 1;

    // Main Camera object and Script
    [SerializeField] private Camera myCamera;
    CameraManager myCameraManager;

    [Header ("Level Prefabs and Triggers")]
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private Transform levelParent;

    private void Start()
    {
        //Subscribe to the event published by CameraManager
        myCameraManager = myCamera.GetComponent<CameraManager>();
        myCameraManager.OnTriggerPoint += testing;

        //Spawn first three levels
        initialPosition = new Vector3(0, 0, 96);
        spawnPosition = initialPosition;

        foreach (var levelPrefab in levelPrefabs)
        {
            GameObject parent = Instantiate(levelPrefab, spawnPosition, Quaternion.identity);
            spawnPosition = new Vector3(0, 0, initialPosition.z + offsetAmount * chunkCounter);
            chunkCounter++;
            parent.transform.SetParent(levelParent);
        }

    }

    private void testing(object sender, EventArgs e)
    {
        Debug.Log("Triggered");
    }
}
