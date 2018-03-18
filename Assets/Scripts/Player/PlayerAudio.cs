using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    PlayerController playerController;

    public AudioClip[] softSteps;
    public AudioClip[] hardSteps;
    public AudioClip[] woodSteps;
    public AudioClip[] mudSteps;

    public AudioSource audioSource;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void PlayStep()
    {
        switch(playerController.currentSurface)
        {
            case surfaceType.Ground:
                PlayRandomFromArray(softSteps);
                break;
            case surfaceType.Wood:
                PlayRandomFromArray(woodSteps);
                break;
        }
    }


    public void PlayRandomFromArray(AudioClip[] myArray)
    {
        int index = Random.Range(0, myArray.Length);

        audioSource.PlayOneShot(myArray[index]);
    }
}
