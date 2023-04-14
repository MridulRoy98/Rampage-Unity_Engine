using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> zombies;

    private void Start()
    {
        SpawnZombies();
    }

    private void SpawnZombies()
    {
        int randomIndex = Random.Range(0, zombies.Count);
        Instantiate(zombies[randomIndex], this.transform.position, Quaternion.identity);
    }
}
