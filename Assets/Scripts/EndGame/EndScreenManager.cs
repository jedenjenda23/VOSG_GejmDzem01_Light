using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public float timeInFloat;
    public int time;

    string uiTime;
    public Text uiTimeText;
    public Text uiNameText;

    void Awake()
    {
        if(playerUI.instance != null)
        {
            playerUI.instance.timerActive = false;
            uiTime = playerUI.instance.timerText.text;
            timeInFloat = playerUI.instance.timeInFloat;

            time = Mathf.RoundToInt(timeInFloat);

            uiTimeText.text = uiTime;

            Destroy(playerUI.instance.gameObject);
        }
    }


    public void SendScore()
    {
        if (uiNameText.text != "")
        {
            ScoreManager.instance.AddNewPlayerScore(uiNameText.text, uiTime, Mathf.RoundToInt(timeInFloat));
            SceneManager.LoadScene("scene_mainMenu");
        }
    }
}

