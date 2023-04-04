using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class LevelPooling : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 spawnPosition;
    private int offsetAmount = -48;
    private int chunkCounter = 1;

    [Header ("Level Prefabs and Triggers")]
    [SerializeField] private GameObject[] levelPrefabs;
    [SerializeField] private Transform levelParent;
    [SerializeField] private Vector3 BackTrigger;
    [SerializeField] private Vector3 SpawnPoint;

    private void Start()
    {
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
}
