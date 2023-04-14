using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> zombies;
    [SerializeField] private GameObject spawnLocation;


    private void Start()
    {
        for (int i = 0; i < zombies.Count; i++)
        {

        }
    }
}
