using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    private Animator playerAnimator;
    private CharacterController cc;
    private List <GameObject> detectedZombies;

    [Header("Enemy Detection Stats")]
    [SerializeField] private int checkSphereRadius = 5;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float aimSpeed = 10f;

    [Header("Character Movement Stats")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotateSpeed = 10f;

    void Start()
    {
        detectedZombies = new List<GameObject>();
        cc = GetComponent<CharacterController>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isMoving())
        {
            Move();
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
            ShootClosestEnemy();
        }
    }

    private bool isMoving()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Checks for enemies within a specific range and adds to list
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
                    Debug.Log("Number of enemies in sight: " + detectedZombies.Count);
                }
            }
        }
    }

    //Returns the closest enemy from the enemy list
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

    //Aims towards the closest enemy from the list
    private void ShootClosestEnemy()
    {
        CheckEnemiesWithinRadius();
        GameObject target = FindClosestEnemy();


        //Multiplied by Quaternion.Euler(0, 180, 0), because the scene is rotated and vector forward of the player is 180 rotated
        if (target != null)
        {
            Vector3 enemyDirection = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(enemyDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, (lookRotation * Quaternion.Euler(0, 170, 0)), aimSpeed * Time.deltaTime);
        }
    }

    //Visualize the range
    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkSphereRadius);
    }

    //Handles player input and character movement
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
