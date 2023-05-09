using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    private Animator playerAnimator;
    private CharacterController cc;
    private List <GameObject> detectedZombies;

    [Header("Enemy Detection Criteria")]
    [SerializeField] private int checkSphereRadius = 5;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Character Movement Stats")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotateSpeed = 10f;

    void Start()
    {
        detectedZombies = new List<GameObject>();
        cc = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private bool isMoving()
    {
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        ShootClosestEnemy();
        //if (isMoving())
        //{
        //    Move();
        //}
        //else
        //{
        //    playerAnimator.SetBool("isRunning", false);
        //    CheckEnemiesWithinRadius();
        //    ShootClosestEnemy();
        //}
    }

    private void CheckEnemiesWithinRadius()
    {
        if (Physics.CheckSphere(transform.position, checkSphereRadius, enemyLayer))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkSphereRadius, enemyLayer);

            // Loop through all colliders within the sphere
            foreach (Collider collider in colliders)
            {
                if (!detectedZombies.Contains(collider.gameObject))
                {
                    detectedZombies.Add(collider.gameObject);
                    Debug.Log(detectedZombies.Count);
                }
            }
        }
    }

    private GameObject FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (var detectedZombie in detectedZombies)
        {
            if(detectedZombie != null)
            {
                float tempDistance = Vector3.Distance(transform.position, detectedZombie.transform.position);
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    closestEnemy = detectedZombie;
                }
            }
        }
        return closestEnemy; 
    }
    private void ShootClosestEnemy()
    {
        CheckEnemiesWithinRadius();
        GameObject target = FindClosestEnemy();

        if(target != null)
        {
            Vector3 enemyDirection = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(enemyDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, (lookRotation * Quaternion.Euler(0, 180, 0)), rotateSpeed);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkSphereRadius);
    }


    private void Move()
    {   
        //Get Player Input
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        //Move the player
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        cc.SimpleMove(-moveDirection * moveSpeed);

        //Rotate the player
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation , rotateSpeed);
        }

        //Trigger Animation
        if(moveDirection != Vector3.zero)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else playerAnimator.SetBool("isRunning", false);
    }


    //For zombies to get player's location
    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
}
