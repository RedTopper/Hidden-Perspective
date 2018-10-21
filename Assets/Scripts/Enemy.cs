using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform player;               // Reference to the player's position.
    NavMeshAgent nav;


    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        NavMeshHit closestHit;
        if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
        {
            gameObject.transform.position = closestHit.position;
        }
        else
        {
            Debug.Log("Really could not find navmesh :(");
        }
    }


    void Update()
    {
        // ... set the destination of the nav mesh agent to the player.
        //Maze maze = GameObject.Find("Maze Runner").GetComponent<Maze>();
        nav.SetDestination(player.position);
       
    }
}
