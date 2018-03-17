using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionOnColor : LightColorInteractible
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerLight>() != null)
        {
            if (collision.collider.GetComponent<PlayerLight>().currentLightColor == currentLightColor)
            {
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.collider, true);
                StartCoroutine("ReturnCollision", collision.collider);
            }


        }
    }

    IEnumerator ReturnCollision(Collider col)
    {
        yield return new WaitForSeconds(1.5f);
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), col, false);
    }


}
