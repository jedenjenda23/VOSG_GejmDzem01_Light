using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour {
    public static playerUI instance;
    public static Music instancA;

    public AnimationCurve curve;
    public AudioMixer masterMixer;
    public PlayerLight player;

    AudioSource[] sources;              // List of references to Audio Sources
    [Range(0, 100)]
    public float lightValue = 100;      // Value of light
    [Range(0, 7)]
    public int activeSource = 0;        // Currently active source
    [Range(0.0001f, 1)]
    public float fadein = 0.4f;         // Fadein time
    [Range(0.0001f, 1)]
    public float fadeout = 0.02f;       // Fadeout time
    [Range(0.0001f, 1)]
    public float fadeoutCutoff = 0.01f; // When fading out should turn into 0
    public float offset = 0;
    public int cas = 0;
    public int casChange = 0;
    public bool canChange = false;
    public bool changeNow = false;
    public bool[] stateOfSources = new bool[]  { true,  false, false, false, false, false, false, false };
    [Range(0,1)]
    public float[] volume;              // Volumes of Audio Sources in inspector

    private void Awake()
    {
        if (Music.instancA != null) Destroy(gameObject);
        else Music.instancA = this;
    }

    void Start () {
        player = PlayerController.playerObject.GetComponent<PlayerLight>();

        sources = GetComponents<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = 0;
            volume[i] = sources[i].volume;
        }

        sources[0].volume = 1;
    }
	
	void Update () {
        float currentLight = 0;

        if (PlayerController.playerObject != null)
        {
            currentLight = ((player.remainingLight / 15) * (-8)) + 9;
            if (currentLight >= 7) currentLight = 7;
            if (currentLight < 0.01) currentLight = 0;
        }

        cas = Mathf.FloorToInt((Time.time*2) % 15);
        if (cas == 0) casChange = 0;
        if (CanChange())
        {
            stateOfSources[activeSource] = false;
            activeSource = Mathf.FloorToInt(currentLight);
            stateOfSources[activeSource] = true;
        }

        SetVolumes();
        GetVolumes();
        if (changeNow) Change();

        masterMixer.SetFloat("Cutoff", (curve.Evaluate(((currentLight / 8)* (currentLight / 8))))*30000+1000);
        Debug.Log((currentLight / 8) * (currentLight / 8));
    }

    bool CanChange()
    {
        if (cas > casChange || changeNow)
        {
            casChange += 1;
            return true;
        }
        else return false;
    }

    void SetVolumes()
    {
        ChangeVolume(0,fadein,fadeout,1f);
        ChangeVolume(1,fadein,fadeout,1f);
        ChangeVolume(2,fadein,fadeout,1f);
        ChangeVolume(3,fadein,fadeout,1f);
        ChangeVolume(4,fadein,fadeout,1.2f);
        ChangeVolume(5,fadein,fadeout,1.5f);
        ChangeVolume(6,fadein,fadeout,1.7f);
        ChangeVolume(7,fadein,fadeout,2f);
    }
    void ChangeVolume(int x, float fadeIn, float fadeOut, float scale)
    {
        //if (stateOfSources[x]) sources[x].volume = Mathf.SmoothStep(sources[x].volume, 1, fadeOut * scale);
        if (stateOfSources[x]) sources[x].volume = Mathf.Lerp(sources[x].volume, 1, fadeOut * scale);
        else
        {
            if (sources[x].volume < fadeoutCutoff) sources[x].volume = 0;
            else sources[x].volume = Mathf.Lerp (sources[x].volume, 0,fadeOut * scale);
        }
    }
    void GetVolumes()
    {
        for (int i = 0; i < sources.Length; i++)
        {
            volume[i] = sources[i].volume;
        }
    }
    void KillAll()
    {
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].volume = 0;
        }
    }

    private void Change()
    {
        changeNow = false;
        ChangeVolume(0, 1,0.1f,1);
        ChangeVolume(1, 1,0.1f,1);
        ChangeVolume(2, 1,0.1f,1);
        ChangeVolume(3, 1,0.1f,1);
        ChangeVolume(4, 1,0.1f,1);
        ChangeVolume(5, 1,0.1f,1);
        ChangeVolume(6, 1,0.1f,1);
        ChangeVolume(7, 1,0.1f,1);
    }
    public void ChangeRightNow()
    {
        player = PlayerController.playerObject.GetComponent<PlayerLight>();
        changeNow = true;
    }
}
