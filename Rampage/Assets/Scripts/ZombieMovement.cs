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

    private bool isChasing = false;
    [SerializeField] private GameObject destroyer;

    private void Awake()
    {
        player = GameObject.Find("Character");
        playermovement = player.GetComponent<PlayerMovement>();
        zombieAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        surface = navmeshSurface.GetComponent<NavMeshSurface>();
    }
    private void Start()
    {
        destroyer = GameObject.Find("Zombie_Destroyer");
    }
    void Update()
    {
        setSpeed();
        ChaseMode();
        if (isChasing == true)
        {
            FollowPlayer();
            
        }
        else
        {
            Attack();
        }
    }

    private void ChaseMode()
    {
        float distance = Vector3.Distance(transform.position, playermovement.GetPlayerPosition());
        if (distance > 1f)
        {
            zombieAnimator.SetBool("zombie_attack", false);
            zombieAnimator.SetBool("zombie_slam", false);
            isChasing = true;
        }
        else
        {
            isChasing=false;
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
                isChasing = false;
                break;
                
            case 1:
                zombieAnimator.SetBool("zombie_slam", true);
                isChasing = false;
                break;
        }
    }
    private void setSpeed()
    {
        if (zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Walk"))
        {
            agent.speed = Random.Range(0.4f, 0.5f);
            isChasing = true;
        }
        else if (zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Run"))
        {
            agent.speed = Random.Range(1.4f, 1.6f);
            isChasing = true;
        }
        else if (zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Crawl"))
        {
            agent.speed = Random.Range(2.8f, 3f);
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
    }
    private void chasingAnimation()
    {
        int animNumber = Random.Range(0, 3);
        switch (animNumber)
        {
            case 0:
                zombieAnimator.SetBool("zombie_walk", true);
                break;

            case 1:
                zombieAnimator.SetBool("zombie_run", true);
                break;

            case 2:
                zombieAnimator.SetBool("zombie_crawl", true);
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == destroyer.name)
        {
            Destroy(gameObject);
        }
    }
}
