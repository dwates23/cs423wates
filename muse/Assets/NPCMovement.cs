using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    private Transform player;
    private NavMeshAgent agent;
    private float timer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Make sure your player has a "Player" tag.
        timer = wanderTimer;

        // Start the wandering coroutine.
        StartCoroutine(Wander());
    }

    private void Update()
    {
        // Check if the player is within a certain range and take some action.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < 5f)
        {
            // Implement your behavior when the player is nearby.
            // For example, you could stop moving or engage in some action.
            agent.isStopped = true;
        }
        else
        {
            // Resume movement if the player is not nearby.
            agent.isStopped = false;
        }
    }

    private IEnumerator Wander()
    {
        while (true)
        {
            // Generate a random position within the wander radius.
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);

            // Set the destination for the NPC.
            agent.SetDestination(newPos);

            // Wait for a certain amount of time before generating a new destination.
            yield return new WaitForSeconds(wanderTimer);
        }
    }

    // Generate a random point within a sphere.
    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
