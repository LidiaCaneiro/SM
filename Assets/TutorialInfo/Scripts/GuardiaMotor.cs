using UnityEngine;
using UnityEngine.AI;

public class GuardMotor : MonoBehaviour
{
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position, float speed)
    {
        agent.speed = speed;
        agent.destination = position;
    }

    public bool HasArrived()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    public Vector3 Velocity()
    {
        return agent.velocity;
    }
}