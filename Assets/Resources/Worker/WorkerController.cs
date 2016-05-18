using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkerController : MonoBehaviour {

    public BurritoController burritoPrefab;

    private Animator animator;
    private NavMeshAgent agent;
    
    private Waypoint currentDest;
    private Station currentStation;

    private List<string> taskList = new List<string>();

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
        createBurrito();
    }

    Station FindStation(string name)
    {
        return GameUtil.SafeFind("Stations").SafeFindChild(name).SafeGetComponent<Station>();
    }

    public void AssignTask(string name)
    {
        taskList.Add(name);
        UpdateTask();
    }

    void UpdateTask()
    {
        if (taskList.Count > 0 && currentStation == null)
        {
            string dest = taskList[0];
            taskList.RemoveAt(0);
            currentStation = FindStation(dest);
            MoveToStation(dest);
        }
    }

    /**
        Move the worker to the named Station
    */
    public void MoveToStation(string name)
    {
        GameObject station = GameUtil.SafeFind("Stations").SafeFindChild(name);
        currentDest = station.SafeGetComponent<Station>().GetWorkerWaypoint();
        agent.SetDestination(currentDest.position);
    }

    public void TransferIngredient()
    {
        if (currentStation != null)
        {
            if (currentStation.name.Equals("Money"))
            {
                GameObject currentBurrito = GameUtil.SafeFind("Burrito");
                Destroy(currentBurrito);
                createBurrito();
            }
        }
        
    }

    public void createBurrito()
    {
        // Create a burrito along with the customer
        BurritoController burrito = Instantiate<BurritoController>(burritoPrefab);
        Waypoint burritoStart = GameUtil.SafeFind("BurritoEnterWaypoint").SafeGetComponent<Waypoint>();
        float burritoX = GameUtil.SafeFind("Worker").transform.position.x;
        burrito.transform.position.Set(burritoX, burritoStart.position.y, burritoStart.position.z);
    }

    void Update()
    {
        if (currentDest != null)
        {
            Vector3 velocity = (agent.nextPosition - transform.position) / Time.deltaTime;
            float dx = Vector3.Dot(transform.right, velocity);
            float dy = Vector3.Dot(transform.forward, velocity);

            animator.SetFloat("x_velocity", dx);
            animator.SetFloat("z_velocity", dy);

            if (currentDest != null)
            {
                float targetDistance = (transform.position - currentDest.position).magnitude;
                if (targetDistance < 1f)
                {
                    // Set current station, if this waypoint is for a Station
                    if (currentDest.transform.parent != null
                                && currentDest.transform.parent.GetComponent<Station>() != null)
                        currentStation = currentDest.transform.parent.SafeGetComponent<Station>();

                    if (ReachedDestination != null)
                        ReachedDestination(this, currentDest, currentDest.transform.parent.name);
                    currentDest = null;
                }
            }
            
        }
        else
        {
            animator.SetFloat("x_velocity", 0);
            animator.SetFloat("z_velocity", 0);

            // TODO do progress on the serving animation
            // TODO if the server animation is complete, add the ingredient to the burrito, and go to the next station
            currentStation = null;
            UpdateTask();
        }

        transform.position = agent.nextPosition;

        // Move burrito with worker
        GameObject burrito = GameObject.Find("Burrito");
        if (burrito != null)
        {
            burrito.transform.position.Set(transform.position.x, burrito.transform.position.y,
                burrito.transform.position.z);
            transform.position = agent.nextPosition;
        }
    }
}
