using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject destroyer;
    PlayerMovement playermovement;

    [Header("Navmesh things")]
    private NavMeshAgent agent;
    public GameObject navmeshSurface;
    private NavMeshSurface surface;

    [Header("Zombie Stats")]
    private Animator zombieAnimator;
    private float rotationSpeed = 1f;
    private bool isChasing = false;
    private bool canDamage = false;


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
        ModeSelection();
        setSpeed();
        if (isChasing)
        {
            FollowMode();
        }
        else
        {
            AttackMode();
        }

    }

    //Deciding whether the zombie should chase or attack
    private void ModeSelection()
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

    //Chasing
    private void FollowMode()
    {
        agent.SetDestination(playermovement.GetPlayerPosition());
        Vector3 lookDirection = playermovement.GetPlayerPosition() - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        chasingAnimation();
    }

    //Attacking
    private void AttackMode()
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

    //Randomize speed for different zombie animations
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

    //Randomize chase animation
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

    //Destroys zombie when too far from player
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == destroyer.name)
        {
            Destroy(gameObject);
        }
    }

    ///////////////////////////////////////////////////////////
    //Combat Mode//
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject == player && canDamage == true)
        {
            //Trigger Event
        }
    }

    //Animation Events//
    private bool DamageOn()
    {
        canDamage = true;
        return true;
    }
    private bool DamageOff()
    {
        canDamage = false;
        return true;
    }
    /////////////////////////
}
