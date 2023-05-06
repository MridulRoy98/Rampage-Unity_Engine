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
        if (isMoving())
        {
            Move();
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
            CheckEnemiesWithinRadius();
            FindClosestEnemy();
            //ShootClosestEnemy();
        }
    }

    private void CheckEnemiesWithinRadius()
    {
        if (Physics.CheckSphere(transform.position, checkSphereRadius, enemyLayer))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkSphereRadius, enemyLayer);

            // Loop through all colliders within the sphere
            foreach (Collider collider in colliders)
            {
                detectedZombies.Add(collider.gameObject);
            }
        }
    }

    private GameObject FindClosestEnemy()
    {
        float distance = 99f;
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
        Debug.Log(closestEnemy);
        Vector3 enemyDirection = closestEnemy.transform.position - this.transform.position;
        float angleToEnemy = Mathf.Atan2(enemyDirection.y, enemyDirection.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angleToEnemy, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);
        return closestEnemy; 
    }
    private void ShootClosestEnemy()
    {
        GameObject target = FindClosestEnemy();
        Vector3 enemyDirection = target.transform.position - transform.position;
        float angleToEnemy = Mathf.Atan2(enemyDirection.x, enemyDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angleToEnemy, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);
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
