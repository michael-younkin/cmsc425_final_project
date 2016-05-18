using UnityEngine;
using System.Collections;

public class CustomerController : MonoBehaviour {

    // TODO List of desired ingredients

    private Animator animator;
    private NavMeshAgent agent;

    private GameObject path;
    private Waypoint currentDest;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        path = GameUtil.SafeFind("CustomerPath");
        currentDest = path.SafeGetComponent<Path>().getStart();

        agent.SetDestination(currentDest.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = (agent.nextPosition - transform.position) / Time.deltaTime;
        animator.SetFloat("x_velocity", velocity.x);
        animator.SetFloat("z_velocity", velocity.z);

        float targetDistance = (transform.position - currentDest.position).magnitude;
        if (targetDistance < 0.5f)
        {
            Debug.Log("Reached target");
            currentDest = currentDest.getNext();
            agent.SetDestination(currentDest.position);
        }
    }
}
