using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnColor : LightColorInteractible
{
    new void Start()
    {
        switch (currentLightColor)
        {
            case lightTypes.Standart:
                GetComponent<MeshRenderer>().material = GlobalColorManager.instance.standartEmmisive;
                break;
            case lightTypes.Red:
                GetComponent<MeshRenderer>().material = GlobalColorManager.instance.redEmmisive;
                break;
            case lightTypes.Green:
                GetComponent<MeshRenderer>().material = GlobalColorManager.instance.greenEmmisive;
                break;
            case lightTypes.Blue:
                GetComponent<MeshRenderer>().material = GlobalColorManager.instance.blueEmmisive;
                break;
        }
    }

    public override void Interact(lightTypes lightColor)
    {
        if(lightColor == currentLightColor)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerLight>().currentLightColor == currentLightColor)
        {
            Destroy(gameObject);
        }
    }
}
