using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardPanel : MonoBehaviour
{
    public static ScoreboardPanel instance;

    public RectTransform scoreboardPanel;
    public GameObject playerScoreObject;
    List<GameObject> lines = new List<GameObject>();

    void Awake()
    {
        if (ScoreboardPanel.instance != null) Destroy(gameObject);
        else ScoreboardPanel.instance = this;
    }

    public void DrawScores(int scoreboardSize, List<PlayerScore> playerScores)
    {
        if (lines.Count > 0)
        {
            foreach (GameObject obj in lines)
            {
                Destroy(obj);
            }
        }

        lines.Clear();

        for (int i = 0; i < scoreboardSize; i++)
        {
            GameObject newScoreObj = Instantiate(playerScoreObject, scoreboardPanel);
            newScoreObj.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);

            lines.Add(newScoreObj);

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
