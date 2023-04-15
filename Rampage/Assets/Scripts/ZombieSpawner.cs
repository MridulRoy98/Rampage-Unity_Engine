using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> zombies;
    [SerializeField] private List<GameObject> spawned;
    
    private void Start()
    {
        spawned = new List<GameObject>();
        SpawnZombies();
    }
    private void SpawnZombies()
    {
        int randomIndex = Random.Range(0, zombies.Count);
        GameObject spawnedZombie = Instantiate(zombies[randomIndex], this.transform.position, Quaternion.identity);
        spawned.Add(spawnedZombie);
    }
}
