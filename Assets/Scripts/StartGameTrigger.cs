using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerLight>())
        {
            other.GetComponent<PlayerLight>().gameStarted = true;
        }
    }
}
