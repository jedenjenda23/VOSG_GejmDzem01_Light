using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : LightPickup
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerLight>())
        {
            lightTypes tempCol = lightColor;

            //Switch Colors
            lightColor = other.GetComponent<PlayerLight>().currentLightColor;
            other.GetComponent<PlayerLight>().currentLightColor = tempCol;
            UpdateLightColor();
        }
    }
}
