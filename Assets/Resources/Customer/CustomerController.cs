using UnityEngine;
using System.Collections.Generic;

public class CustomerController : MonoBehaviour {

    // TODO List of desired ingredients

    private Animator animator;
    private NavMeshAgent agent;

    private GameObject path;
    private Waypoint currentDest;
    private bool exiting;

    public delegate void ReachedDestinationEvent(Waypoint dest);
    public event ReachedDestinationEvent ReachedDestination;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        currentDest = null;
        exiting = false;
    }

    /**
        Move the customer to the named Bin
    */
    public void MoveToStation(string name)
    {
        GameObject station = GameUtil.SafeFind("Stations").SafeFindChild(name);
        currentDest = station.SafeGetComponent<Station>().GetCustomerWaypoint();
        agent.SetDestination(currentDest.position);
        exiting = false;
    }

    /**
        Move the customer to the exit waypoint
    */
    public void MoveToExit()
    {
        GameObject waypoint = GameUtil.SafeFind("ExitWaypoint");
        currentDest = waypoint.SafeGetComponent<Waypoint>();
        agent.SetDestination(currentDest.position);
        exiting = true;
    }
    
    // Update is called once per frame
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
                    ReachedDestination(currentDest);
                currentDest = null;
                if (exiting)
                {
                    Destroy(gameObject);
                }
            }
        }

        transform.position = agent.nextPosition;
    }
}
