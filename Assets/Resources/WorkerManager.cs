using UnityEngine;
using System.Collections.Generic;

public class WorkerManager : MonoBehaviour {

    private List<WorkerController> workers;

    public WorkerController workerPrefab;
    public int numWorkers = 3;

	// Use this for initialization
	void Start() {
        workers = new List<WorkerController>();
        for (int i = 0; i < numWorkers; i++)
        {
            WorkerController worker = Instantiate(workerPrefab);
            worker.transform.position = new Vector3(i * (20.0f / (numWorkers - 1)), 0.0f, 0.0f);
            worker.ReachedDestination += Worker_ReachedDestination;
            workers.Add(worker);
        }
	}

    public bool GetIngredient(string ingredient)
    {
        return false;
    }

    private void Worker_ReachedDestination(WorkerController worker, Waypoint dest, string dest_id)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
