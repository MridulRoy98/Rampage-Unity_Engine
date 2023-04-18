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
        agent.speed = Random.Range(0.2f, 0.6f);
    }
    void Update()
    {
        AttackAnimation();
    }
    private void FollowPlayer()
    {
        agent.SetDestination(playermovement.GetPlayerPosition());
        Vector3 lookDirection = playermovement.GetPlayerPosition() - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void AttackAnimation()
    {
        float distance = Vector3.Distance(playermovement.GetPlayerPosition(), transform.position);
        if (distance < 1.5f)
        {
            zombieAnimator.SetBool("zombie_attack", true);
            Debug.Log(distance);
            //int animNumber = Random.Range(0, 2);
            //switch (animNumber)
            //{
            //    case 0:
            //        zombieAnimator.SetBool("zombie_attack", true);
            //        break;
            //    case 1:
            //        zombieAnimator.SetBool("zombie_neckbite", true);
            //        break;
            //}
            
        }
        else
        {
            zombieAnimator.SetBool("zombie_attack", false);
            //zombieAnimator.SetBool("zombie_neckbite", false);
            FollowPlayer();
        }
    }
    private void chasingAnimation()
    {
        int animNumber = Random.Range(0, 2);

        switch (animNumber)
        {
            case 0:
                zombieAnimator.SetBool("zombie_run", true);
                break;
            case 1:
                zombieAnimator.SetBool("zombie_crawl", true);
                break;
        }

    }


}
