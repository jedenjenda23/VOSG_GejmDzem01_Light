using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightPickup : MonoBehaviour
{
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
            other.GetComponent<PlayerLight>().currentLightColor = lightColor;
            other.GetComponent<PlayerLight>().AddRemainingLight(lightAmount);
             if(lightColor == lightTypes.Standart && !destroyParent) Destroy(gameObject);
             else if (lightColor == lightTypes.Standart && destroyParent) Destroy(transform.parent.gameObject);

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
