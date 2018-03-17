﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightPickup : MonoBehaviour
{
    [SerializeField]
    [Range(0, 120)]
    protected float lightAmount = 60f;

    [SerializeField]
    protected lightTypes lightColor;

    bool updateLightInEditor = true;

    void Awake()
    {
        updateLightInEditor = false;
        UpdateLightColor();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerLight>())
        {
            other.GetComponent<PlayerLight>().currentLightColor = lightColor;
            other.GetComponent<PlayerLight>().AddRemainingLight(lightAmount);
             if(lightColor == lightTypes.Standart) Destroy(gameObject);
        }
    }

    protected virtual void UpdateLightColor()
    {
        switch (lightColor)
        {
            case lightTypes.Standart:
                GetComponentInChildren<MeshRenderer>().material = GlobalColorManager.instance.standartEmmisive;
                GetComponentInChildren<Light>().color = GlobalColorManager.instance.standarLightColor;
                break;

            case lightTypes.Red:
                GetComponentInChildren<Light>().color = GlobalColorManager.instance.redLightColor;
                GetComponentInChildren<MeshRenderer>().material = GlobalColorManager.instance.redEmmisive;
                break;

            case lightTypes.Green:
                GetComponentInChildren<Light>().color = GlobalColorManager.instance.greenLightColor;
                GetComponentInChildren<MeshRenderer>().material = GlobalColorManager.instance.greenEmmisive;
                break;

            case lightTypes.Blue:
                GetComponentInChildren<Light>().color = GlobalColorManager.instance.blueLightColor;
                GetComponentInChildren<MeshRenderer>().material = GlobalColorManager.instance.blueEmmisive;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if (updateLightInEditor)
        {
            UpdateLightColor();
        }
    }
}
