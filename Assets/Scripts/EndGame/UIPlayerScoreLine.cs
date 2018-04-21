using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerScoreLine : MonoBehaviour
{
    public Text playerNameText;
    public Text playerTimeText;

    public void UpdateScoreText(string name, string time)
    {
        playerNameText.text = name;
        playerTimeText.text = time;
    }
}
