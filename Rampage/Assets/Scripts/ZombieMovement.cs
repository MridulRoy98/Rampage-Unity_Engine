using Unity.AI.Navigation;
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
    private void Awake()
    {
        player = GameObject.Find("Character");
        playermovement = player.GetComponent<PlayerMovement>();
    }
    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        surface = navmeshSurface.GetComponent<NavMeshSurface>();
        chasingAnimation();
    }
    void Update()
    {
        Attack();
    }
    private void FollowPlayer()
    {
        agent.SetDestination(playermovement.GetPlayerPosition());
        Vector3 lookDirection = playermovement.GetPlayerPosition() - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        float distance = Vector3.Distance(playermovement.GetPlayerPosition(), transform.position);
        if (distance < 1.2f)
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
        else
        {
            zombieAnimator.SetBool("zombie_attack", false);
            zombieAnimator.SetBool("zombie_slam", false);
            chasingAnimation();
        }
    }
    private void chasingAnimation()
    {
        int animNumber = Random.Range(0, 3);

        switch (animNumber)
        {
            //Walking
            case 0:
                agent.speed = 0.5f;
                zombieAnimator.SetBool("zombie_walk", true);
                zombieAnimator.SetBool("zombie_crawl", false);
                zombieAnimator.SetBool("zombie_run", false);
                FollowPlayer();
                break;

            //Running
            case 1:
                agent.speed = 2.8f;
                zombieAnimator.SetBool("zombie_walk", false);
                zombieAnimator.SetBool("zombie_crawl", false);
                zombieAnimator.SetBool("zombie_run", true);
                FollowPlayer();
                break;

            //Crawling
            case 2:
                agent.speed = 3f;
                zombieAnimator.SetBool("zombie_walk", false);
                zombieAnimator.SetBool("zombie_crawl", true);
                zombieAnimator.SetBool("zombie_run", false);
                FollowPlayer();
                break;
        }

    }


}
