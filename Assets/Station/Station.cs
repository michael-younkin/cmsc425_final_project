using UnityEngine;
using System.Collections;

public class Station : MonoBehaviour {

    public Waypoint GetCustomerWaypoint()
    {
        return gameObject.SafeFindChild("CustomerWaypoint").SafeGetComponent<Waypoint>();
    }

    public Waypoint GetWorkerWaypoint()
    {
        return gameObject.SafeFindChild("WorkerWaypoint").SafeGetComponent<Waypoint>();
    }
}
