using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum actions { None, Destroy }

public class WaypointClass : MonoBehaviour
{

    public actions wpAction = actions.None;

    [Range(0, 20)]
    public float waitingTime = 0f;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        if(wpAction == actions.Destroy)
        {
            UnityEditor.Handles.Label(transform.position, "Destroy");
            Gizmos.color = Color.red;
        }


        else if(wpAction == actions.None)
        {
            if (waitingTime > 0)
            {
                UnityEditor.Handles.Label(transform.position, "Wait: " + waitingTime);
            }
            Gizmos.color = Color.yellow;
        }

        Gizmos.DrawSphere(transform.position, 0.35f);
    }
#endif 
}
