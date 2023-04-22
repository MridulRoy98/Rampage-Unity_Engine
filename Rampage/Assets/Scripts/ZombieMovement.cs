using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    PlayerMovement playermovement;

    private NavMeshAgent agent;
    private Animator zombieAnimator;
    private float rotationSpeed = 1f;
    public GameObject navmeshSurface;
    private NavMeshSurface surface;

    private bool canMove;

    private void Awake()
    {
        player = GameObject.Find("Character");
        playermovement = player.GetComponent<PlayerMovement>();
        zombieAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        surface = navmeshSurface.GetComponent<NavMeshSurface>();
    }
    void Update()
    {
        CanThePlayerMove();
        if (CanThePlayerMove())
        {
            FollowPlayer();
            zombieAnimator.SetBool("zombie_attack", false);
            zombieAnimator.SetBool("zombie_slam", false);
        }
        if (!CanThePlayerMove())
        {
            Attack();
        }
    }

    private bool CanThePlayerMove()
    {
        float distance = Vector3.Distance(transform.position, playermovement.GetPlayerPosition());
        if (distance > 1f)
        {
            return true;
        }
        else
        {
            Debug.Log("Attack");
            return false;
        }
    }
    private void FollowPlayer()
    {
            agent.SetDestination(playermovement.GetPlayerPosition());
            Vector3 lookDirection = playermovement.GetPlayerPosition() - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            chasingAnimation();
    }

    private void Attack()
    {
        int animNumber = Random.Range(0, 2);
        switch (animNumber)
        {
            case 0:
                zombieAnimator.SetBool("zombie_attack", true);
                break;
                
            case 1:
                zombieAnimator.SetBool("zombie_slam", true);
                break;
        }
    }
    private void chasingAnimation()
    {
        int animNumber = Random.Range(0, 3);

        if(animNumber == 0)
        {
            zombieAnimator.SetBool("zombie_walk", true);
        }
        else if(animNumber == 1)
        {
            zombieAnimator.SetBool("zombie_run", true);
        }
        else
        {
            zombieAnimator.SetBool("zombie_crawl", true);
        }

        if (zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Walk"))
        {
            agent.speed = Random.Range(0.7f, 1f);
        }
        else if (zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Run"))
        {
            agent.speed = Random.Range(1.4f, 1.6f);
        }
        else if (zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Crawl"))
        {
            agent.speed = Random.Range(2.8f, 3f);
        }
    }
}
