using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportLocation;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = teleportLocation.position;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (teleportLocation != null)
        {
            Debug.DrawLine(transform.position, teleportLocation.position, Color.cyan);
        }
    }
#endif
}
