using UnityEngine;
using System.Collections;

public class WorkerController : MonoBehaviour {

    private Animator animator;
    private NavMeshAgent agent;
    
    private Waypoint currentDest;
    private Station currentStation;

    public delegate void ReachedDestinationEvent(WorkerController worker, Waypoint dest, string destID);
    public event ReachedDestinationEvent ReachedDestination;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        currentDest = null;
        currentStation = null;
    }

    /**
        Move the worker to the named Station
    */
    public void MoveToStation(string name)
    {
        GameObject station = GameUtil.SafeFind("Stations").SafeFindChild(name);
        currentDest = station.SafeGetComponent<Station>().GetCustomerWaypoint();
        agent.SetDestination(currentDest.position);
    }

    void Update()
    {
        Vector3 velocity = (agent.nextPosition - transform.position) / Time.deltaTime;
        float dx = Vector3.Dot(transform.right, velocity);
        float dy = Vector3.Dot(transform.forward, velocity);

        animator.SetFloat("x_velocity", dx);
        animator.SetFloat("z_velocity", dy);

        if (currentDest != null)
        {
            float targetDistance = (transform.position - currentDest.position).magnitude;
            if (targetDistance < 0.5f)
            {
                if (ReachedDestination != null)
                    ReachedDestination(this, currentDest, currentDest.transform.parent.name);
                currentDest = null;
            }
        }

        transform.position = agent.nextPosition;
    }
}
