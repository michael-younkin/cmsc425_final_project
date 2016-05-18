using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkerController : MonoBehaviour {

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
                    if (ReachedDestination != null)
                        ReachedDestination(this, currentDest, currentDest.transform.parent.name);
                    currentDest = null;
                }
            }

            transform.position = agent.nextPosition;
        } else
        {
            animator.SetFloat("x_velocity", 0);
            animator.SetFloat("z_velocity", 0);

            // TODO do progress on the serving animation
            // TODO if the server animation is complete, add the ingredient to the burrito, and go to the next station
            currentStation = null;
            UpdateTask();
        }
    }
}
