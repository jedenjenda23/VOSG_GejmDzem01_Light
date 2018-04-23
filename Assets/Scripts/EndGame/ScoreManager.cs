using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int scoreboardSize = 10;
    public List<PlayerScore> loadedScores;

    private void Start()
    {
        if (ScoreManager.instance != null) Destroy(gameObject);
        else ScoreManager.instance = this;

        //SaveNull();
        LoadPlayerScores();
        //SavePlayerScores();
        ScoreboardPanel.instance.DrawScores(scoreboardSize, loadedScores);
    }

    public void Reload()
    {
        SaveNull();
        LoadPlayerScores();
        SavePlayerScores();
        ScoreboardPanel.instance.DrawScores(scoreboardSize, loadedScores);

    }

    [ContextMenu("Reset scoreboard")]
    public void SaveNull()
    {
        for (int s = 0; s < scoreboardSize; s++)
        {
            PlayerPrefs.SetInt("playerTimeTotal" + s, 999999);
            PlayerPrefs.SetString("playerTimeText" + s, "x:xx");
            PlayerPrefs.SetString("playerName" + s, "x");
        }
    }

    public void AddNewPlayerScore(string playerName, string playerTimeText, int playerTimeTotal)
    {
        //get list of player scores(loadedScores) and add new score to list of player scores
        PlayerScore newPlayerScore = new PlayerScore(playerName, playerTimeText, playerTimeTotal);
        loadedScores[loadedScores.Count - 1] = newPlayerScore;
        SavePlayerScores();
    }

    public void SavePlayerScores()
    {
        //sort list of playerscores according to time(int)
        loadedScores.Sort((s1, s2) => s1.playerTimeTotal.CompareTo(s2.playerTimeTotal));

        //rewrite playerprefs
        //Debug.Log("loaded scores" + loadedScores.Count);

        for (int i = 0; i < scoreboardSize; i ++)
        {
            PlayerPrefs.SetInt("playerTimeTotal" + i, loadedScores[i].playerTimeTotal);
            PlayerPrefs.SetString("playerTimeText" + i, loadedScores[i].playerTimeText);
            PlayerPrefs.SetString("playerName" + i, loadedScores[i].playerName);
        }
    } 

    public void LoadPlayerScores()
    {
        List<int> timeTotal = new List<int>();
        List<string> timeTextList = new List<string>();
        List<string> playerNameList = new List<string>();

        for (int s = 0; s < scoreboardSize; s++)
        {
            timeTotal.Add(PlayerPrefs.GetInt("playerTimeTotal" + s));
            timeTextList.Add(PlayerPrefs.GetString("playerTimeText" + s));
            playerNameList.Add(PlayerPrefs.GetString("playerName" + s));
        }

        loadedScores = new List<PlayerScore>();

        for (int i = 0; i < scoreboardSize; i++)
        {
            PlayerScore loadedPlayerScore = new PlayerScore(playerNameList[i], timeTextList[i], timeTotal[i]);
            loadedScores.Add(loadedPlayerScore);
        }
    }
}

public struct PlayerScore
{
    public string playerName;
    public string playerTimeText;
    public int playerTimeTotal;

    public PlayerScore(string name, string timeText, int timeTotal)
    {
        playerName = name;
        playerTimeTotal = timeTotal;
        playerTimeText = timeText;
    }
}
