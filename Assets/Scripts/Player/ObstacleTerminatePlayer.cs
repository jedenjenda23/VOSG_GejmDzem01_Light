using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTerminatePlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<PlayerController>())
        {
            MenuFunctions.instance.RestartCurrentScene();
        }
    }
}
