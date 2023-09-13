using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text highScore, currentScore;
    int score;
    void Start()
    {
        TMP_Text killsText = GameObject.Find("Kill Counter").GetComponent<TMP_Text>();
        score = int.Parse(killsText.text);
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Scores",
            MaxResultsCount = 1

        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetHighscore, null);        
    }


    void SaveData()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate {
                StatisticName = "Scores",
                Value = score
            }
        }
        }, null, null);
    }

    void OnGetHighscore(GetLeaderboardAroundPlayerResult result)
    {
        int hScore;
        try
        {
            hScore = result.Leaderboard[0].StatValue;
        }
        catch
        {
            hScore = 0;
        }

        if (hScore < score) {
            hScore = score;
            SaveData();
        } 
        currentScore.text = "CURRENT SCORE: " + score.ToString() + " KILLS";
        highScore.text = "HIGH SCORE: " + hScore.ToString() +" KILLS";
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
