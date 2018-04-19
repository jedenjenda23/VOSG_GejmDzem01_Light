using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneTrigger : MonoBehaviour
{

    public string targetScene = "Onion Knight forever...";
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            MenuFunctions.instance.LoadScene(targetScene);
        }
    }
}
