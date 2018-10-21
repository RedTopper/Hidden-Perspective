using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;               // Reference to the player's position.
    NavMeshAgent nav;


    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        // ... set the destination of the nav mesh agent to the player.
        //Maze maze = GameObject.Find("Maze Runner").GetComponent<Maze>();
        nav.SetDestination(player.position);
       
    }
}
