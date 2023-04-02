using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> zombies;
    [SerializeField] private List<GameObject> spawnLocations;


    private void Start()
    {
        foreach (var location in spawnLocations)
        {
            Instantiate(zombies[Random.Range(0, zombies.Count)], location.transform.position, Quaternion.identity);
        }
    }
}
