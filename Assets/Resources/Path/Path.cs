using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {
    
    public Waypoint GetStart() {
        return transform.SafeFindChild("Waypoint0").SafeGetComponent<Waypoint>();
    }

}
