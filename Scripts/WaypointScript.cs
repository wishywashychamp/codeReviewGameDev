using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    // Private variable to store the next waypoint in the chain
    // It is serializable, so it can be set in the Inspector
    [SerializeField]
    private WaypointScript nextWaypoint;

    //Function to retrieve the next waypoint in the chain
    public WaypointScript GetNextWaypoint()
    {
        return nextWaypoint;
    }

    //Function to retrieve the position of the waypoint
    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
