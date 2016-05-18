using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    public Transform nextWaypoint;

    public Waypoint GetNext()
    {
        return nextWaypoint.SafeGetComponent<Waypoint>();
    }

    public Vector3 position
    {
        get
        {
            return gameObject.transform.position;
        }
        set
        {
            gameObject.transform.position = value;
        }
    }

}
