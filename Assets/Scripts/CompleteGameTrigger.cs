using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            MenuFunctions.instance.CompleteGame();
        }
    }
}
