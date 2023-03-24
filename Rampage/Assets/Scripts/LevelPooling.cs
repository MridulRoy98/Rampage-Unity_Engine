using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPooling : MonoBehaviour
{
    [SerializeField] private float offsetAmount = 0.2f;
    [SerializeField] private float offsetSpeed = 0.2f;
    [SerializeField] private Transform BackTrigger;
    [SerializeField] private Transform SpawnPoint;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3 (0, 0, transform.position.z + offsetAmount * offsetSpeed * Time.deltaTime);
        
        //Trigger level chunk to move to spawnpoint
        if(transform.position.z >= BackTrigger.position.z)
        {
            transform.position = SpawnPoint.position;
        }
    }
}
