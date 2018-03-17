using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{   
    public lightTypes currentLightColor = 0;

    public Light playerLight;

    [Range(0, 120)]
    public float maxLight = 60f;

    float remainingLight = 60f;
    float initLightIntensity;
    float initLightRadius;

    private void Awake()
    {
        remainingLight = maxLight;
        initLightIntensity = playerLight.intensity;
        initLightRadius = playerLight.range;
    }

    void Update ()
    {
        UpdateLight();
	}

    void UpdateLight()
    {
        if(remainingLight > 0)
        {
            //decrease light
            remainingLight -= 1 * Time.deltaTime;

            float percRemainingLight = GetPercentage(remainingLight, maxLight);

            playerLight.intensity = initLightIntensity * (percRemainingLight * 0.01f);
            playerLight.range = initLightRadius * (percRemainingLight * 0.01f);
        }

        if (remainingLight < 0) remainingLight = 0;


        //Update lightColor

        switch(currentLightColor)
        {
            case lightTypes.Standart:
                playerLight.color = Vector4.Lerp(playerLight.color, GlobalColorManager.instance.standarLightColor, 1 * Time.deltaTime);
                break;
            case lightTypes.Red:
                playerLight.color = Vector4.Lerp(playerLight.color, GlobalColorManager.instance.redLightColor, 1 * Time.deltaTime);
                break;
            case lightTypes.Green:
                playerLight.color = Vector4.Lerp(playerLight.color, GlobalColorManager.instance.greenLightColor, 1 * Time.deltaTime);
                break;
            case lightTypes.Blue:
                playerLight.color = Vector4.Lerp(playerLight.color, GlobalColorManager.instance.blueLightColor, 1 * Time.deltaTime);
                break;
        }


    }

    float GetPercentage(float current, float total)
    {
        return current / total * 100;
    }

    public void AddRemainingLight(float amountOfLight)
    {
        remainingLight += amountOfLight;
        if(remainingLight > maxLight) remainingLight = maxLight;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 lightPos = playerLight.transform.position;
        lightPos.y = transform.position.y - 1f;

        UnityEditor.Handles.DrawWireDisc(lightPos, Vector3.up, playerLight.range);
    }
#endif
}
