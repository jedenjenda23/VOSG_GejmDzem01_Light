using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour
{
    public static playerUI instance;
    public Text timerText;
    public RectTransform lightSlider;

    public bool timerActive;
    public float currentSeconds;
    public float currentMinutes;
    public float timeInFloat;

    private void Awake()
    {
        if (playerUI.instance != null) Destroy(gameObject);
        else playerUI.instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (timerActive)
        {
            timeInFloat += 1 * Time.deltaTime;
            currentSeconds += 1 * Time.deltaTime;
        }

        if(currentSeconds > 60)
        {
            currentMinutes++;
            currentSeconds = 0;
        }

        ///////makin' strings pretty
        string secondsString;
        string minutesString;

        if (Mathf.Round(currentSeconds) < 10)
        {
            secondsString = "0" + Mathf.Round(currentSeconds);
        }

        else secondsString = "" + Mathf.Round(currentSeconds);

        if (Mathf.Round(currentMinutes) < 10)
        {
            minutesString = "0" + Mathf.Round(currentMinutes);
        }

        else minutesString = "" + Mathf.Round(currentMinutes);
        ///////
        timerText.text = minutesString + ":" + secondsString;
        

        if(PlayerController.playerObject != null)
        {
            PlayerLight playerLight = PlayerController.playerObject.GetComponent<PlayerLight>();

            float percentageLight = (playerLight.remainingLight / playerLight.maxLight);
            lightSlider.localScale = new Vector2(percentageLight, 1);
        }
    }


}
