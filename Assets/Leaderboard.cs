using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.SceneManagement;
public class Leaderboard : MonoBehaviour
{
    [SerializeField] GameObject TextObject;
    [SerializeField] Transform Table;
    void Start()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Scores",
            StartPosition = 0,
            MaxResultsCount = 5

        };
        PlayFabClientAPI.GetLeaderboard(request, OnResultGet, null);
    }

    void OnResultGet(GetLeaderboardResult results)
    {
        foreach( var result in results.Leaderboard)
        {
            string s = $"{result.Position+1}. {result.DisplayName}: {result.StatValue} KILLS";
            GameObject data = Instantiate(TextObject, Table);
            data.GetComponent<TMP_Text>().text = s;
        }
    }

    public void Return()
    {
        SceneManager.LoadScene(1);
    }
}
