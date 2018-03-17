using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorInteractible : MonoBehaviour
{
    public lightTypes currentLightColor;

    protected void Start()
    {
        Material mat = GetComponent<MeshRenderer>().material;

        switch (currentLightColor)
        {
            case lightTypes.Standart:
                mat = GlobalColorManager.instance.standartEmmisive;
                break;
            case lightTypes.Red:
                mat = GlobalColorManager.instance.redEmmisive;
                break;
            case lightTypes.Green:
                mat = GlobalColorManager.instance.greenEmmisive;
                break;
            case lightTypes.Blue:
                mat = GlobalColorManager.instance.blueEmmisive;
                break;
        }
    }

    public virtual void Interact(lightTypes lightColor)
    {

    }
}
