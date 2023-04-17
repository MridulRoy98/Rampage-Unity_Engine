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
    }
    void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        agent.SetDestination(playermovement.GetPlayerPosition());
        //Debug.Log();
        //Vector3 lookDirection = playermovement.GetPlayerPosition() - transform.position;
        //Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
