using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> zombies;
    [SerializeField] private List<GameObject> spawned;
    [SerializeField] private GameObject player;
    private void Start()
    {

        spawned = new List<GameObject>();
        SpawnZombies();
    }
    private void Update()
    {
        FollowPlayer();
    }
    private void SpawnZombies()
    {
        int randomIndex = Random.Range(0, zombies.Count);
        GameObject spawnedZombie = Instantiate(zombies[randomIndex], this.transform.position, Quaternion.identity);
        spawned.Add(spawnedZombie);
    }

    private void FollowPlayer()
    {
        foreach( var zombie in spawned)
        {
            zombie.GetComponent<NavMeshAgent>().SetDestination(Vector3.zero);
        }
    }
}
