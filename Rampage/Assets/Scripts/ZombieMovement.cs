using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    PlayerMovement playermovement;

    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Character");
        playermovement = player.GetComponent<PlayerMovement>();
    }
    void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        agent.SetDestination(playermovement.GetPlayerPosition());
    }
}
