using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAICloud : MonoBehaviour
{
    public enum states { Idle, Patrol}
    public states myState = states.Idle;

    public Transform[] waypoints;

    [Range(0, 30)]
    public float travelSpeed = 1f;

    int myWaypoint = 0;
    float waitingTimer = 0;

    private void FixedUpdate()
    {
        StateMachineUpdate();
    }

    void StateMachineUpdate()
    {
        switch(myState)
        {
            case states.Idle:
                break;

            case states.Patrol:
                ActionPatrol();
                break;
        }
    }

    void ActionPatrol()
    {
        if (Vector3.Distance(transform.position, waypoints[myWaypoint].position) < 0.1f)
        {
            if (waypoints[myWaypoint].GetComponent<WaypointClass>() != null)
            {
                if (waypoints[myWaypoint].GetComponent<WaypointClass>().wpAction == actions.Destroy)
                {
                    Destroy(gameObject);
                }

                else
                {
                    waitingTimer += 1 * Time.deltaTime;

                    if (waitingTimer > waypoints[myWaypoint].GetComponent<WaypointClass>().waitingTime)
                    {
                        waitingTimer = 0;
                        myWaypoint++;
                    }
                }
            }
            else
            {
                waitingTimer = 0;
                myWaypoint++;
            }

        }

        /*
        if (Vector3.Distance(transform.position, waypoints[myWaypoint].position) > 0.5f)
        {
            // Calculate direction from character to mouse position
            Vector3 direction = waypoints[myWaypoint].position - transform.position;

            // Direction to rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction, transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            // Spherical interpolation to desired rotation
            transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
        */

        if (myWaypoint > waypoints.Length - 1) myWaypoint = 0;
        // transform.position = Vector3.Lerp(transform.position, waypoints[myWaypoint].position, travelSpeed * Time.deltaTime);

        transform.LookAt(waypoints[myWaypoint]);
        transform.Translate(Vector3.forward * travelSpeed * 0.01f);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for(int i = 1; i < waypoints.Length; i++)
        {
            Debug.DrawLine(waypoints[i].position, waypoints[i - 1].position, Color.green);
        }

        Debug.DrawLine(waypoints[0].position, waypoints[waypoints.Length - 1].position, Color.green);
    }
#endif 
}
