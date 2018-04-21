using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardPanel : MonoBehaviour
{
    public static ScoreboardPanel instance;

    public RectTransform scoreboardPanel;
    public GameObject playerScoreObject;

    void Awake()
    {
        if (ScoreboardPanel.instance != null) Destroy(gameObject);
        else ScoreboardPanel.instance = this;
    }

    public void DrawScores(int scoreboardSize, List<PlayerScore> playerScores)
    {
        for (int i = 0; i < scoreboardSize; i++)
        {
            GameObject newScoreObj = Instantiate(playerScoreObject, scoreboardPanel);
            newScoreObj.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

            //Draw in panel if it's not null score
            if(playerScores[i].playerTimeTotal >= 0)
            {
                newScoreObj.GetComponent<UIPlayerScoreLine>().UpdateScoreText(playerScores[i].playerName, playerScores[i].playerTimeText);
            }

            else
            {
                newScoreObj.GetComponent<UIPlayerScoreLine>().UpdateScoreText("", "");
            }
        }
    }
}
