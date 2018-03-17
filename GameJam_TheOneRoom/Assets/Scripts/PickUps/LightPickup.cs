using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightPickup : MonoBehaviour
{
    [SerializeField]
    [Range(0, 120)]
    float lightAmount = 60f;

    public lightTypes lightColor;

    bool updateLightInEditor = true;

    void Awake()
    {
        updateLightInEditor = false;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerLight>())
        {
            other.GetComponent<PlayerLight>().currentLightColor = lightColor;
            other.GetComponent<PlayerLight>().AddRemainingLight(lightAmount);
             if(lightColor == lightTypes.Standart) Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if(updateLightInEditor)
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
    }
}
