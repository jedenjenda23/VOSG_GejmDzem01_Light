﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightPickup : MonoBehaviour
{
    public AudioClip[] pickupSounds;

    public bool destroyParent;
    [SerializeField]
    [Range(0, 120)]
    protected float lightAmount = 60f;

    [SerializeField]
    [Range(0, 10)]
    protected float speedBoostTime = 0f;

    [SerializeField]
    protected float speedBoostAmount = 0f;

    [SerializeField]
    protected lightTypes lightColor;

    bool updateLightInEditor = true;

    void Awake()
    {
        updateLightInEditor = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerLight>())
        {
            if (lightColor != lightTypes.Standart)other.GetComponent<PlayerLight>().currentLightColor = lightColor;
            other.GetComponent<PlayerLight>().AddRemainingLight(lightAmount);

            if (!destroyParent)
            {
                Destroy(gameObject);

            if (pickupSounds.Length > 0)
                {
                    int index = Random.Range(0, pickupSounds.Length);
                    AudioSource.PlayClipAtPoint(pickupSounds[index], transform.position,1.5f);
                }
            }
             else if (destroyParent)
            {
                Destroy(transform.parent.gameObject);
                if (pickupSounds.Length > 0)
                {
                    int index = Random.Range(0, pickupSounds.Length);
                    AudioSource.PlayClipAtPoint(pickupSounds[index], transform.position);
                }
            }

        }

        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().AddBoostSpeed(speedBoostAmount, speedBoostTime);
        }

    }

    protected void UpdateLightColor()
    {
        switch (lightColor)
        {
            case lightTypes.Standart:
                GetComponentInChildren<Light>().color = Color.white;
                break;

            case lightTypes.Red:
                GetComponentInChildren<Light>().color = Color.red;
                break;

            case lightTypes.Green:
                GetComponentInChildren<Light>().color = Color.green;
                break;

            case lightTypes.Blue:
                GetComponentInChildren<Light>().color = Color.blue;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if(updateLightInEditor)
        {
            UpdateLightColor();
        }
    }
}
