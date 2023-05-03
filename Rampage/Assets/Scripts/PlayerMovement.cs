using System.Collections.Generic;
using UnityEngine;

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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Detected");
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
            checkEnemiesWithinRadius();
        }
    }

    private void checkEnemiesWithinRadius()
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
