using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum lightTypes { Standart, Red, Green, Blue };

public class GlobalColorManager : MonoBehaviour
{
    public static GlobalColorManager instance;

    public Color standarLightColor = Color.white;
    public Color redLightColor = Color.red;
    public Color blueLightColor = Color.blue;
    public Color greenLightColor = Color.green;

    public Material standartEmmisive;
    public Material redEmmisive;
    public Material greenEmmisive;
    public Material blueEmmisive;

    void Awake ()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else instance = this;
	}
	
}
