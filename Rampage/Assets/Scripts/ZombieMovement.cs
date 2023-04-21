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
    void Start()
    {
        canMove = true;
    }
    void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        agent.SetDestination(playermovement.GetPlayerPosition());
        Vector3 lookDirection = playermovement.GetPlayerPosition() - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (agent.isOnOffMeshLink)
        {
            agent.speed = 0f;
        }

        chasingAnimation();
    }

    //private void Attack()
    //{
    //    float distance = Vector3.Distance(playermovement.GetPlayerPosition(), transform.position);
    //    if (distance < 1.2f)
    //    {
    //        agent.speed = 0;
    //        int animNumber = Random.Range(0, 2);
    //        switch (animNumber)
    //        {
    //            case 0:
    //                zombieAnimator.SetBool("zombie_attack", true);
    //                break;

    //            case 1:
    //                zombieAnimator.SetBool("zombie_slam", true);
    //                break;
    //        }
    //        canMove = false;
    //    }
    //    else
    //    {
    //        zombieAnimator.SetBool("zombie_attack", false);
    //        zombieAnimator.SetBool("zombie_slam", false);
    //        canMove = true;
    //    }
    //}
    private void chasingAnimation()
    {
        int animNumber = Random.Range(0, 4);

        if(animNumber == 1)
        {
            zombieAnimator.SetBool("zombie_walk", true);
        }
        else if(animNumber == 2)
        {
            zombieAnimator.SetBool("zombie_run", true);
        }
        else
        {
            zombieAnimator.SetBool("zombie_crawl", true);
        }

        if (zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Walk"))
        {
            Debug.Log("here");
            agent.speed = 1f;
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
