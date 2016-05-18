using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {
    
    public Waypoint getStart() {
        return transform.SafeFindChild("Waypoint0").SafeGetComponent<Waypoint>();
    }

}
