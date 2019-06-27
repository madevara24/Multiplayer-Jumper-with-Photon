using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayGameController : MonoBehaviour
{
    public GameObject[] button;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Google Place Service Start");
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
    }

    public void SignIn()
    {
        Social.localUser.Authenticate((bool success) => {
            if (success == true)
            {
                UnlockAcievement(GPGSIds.achievementLoginGame);
            } 
        });

    }

    #region Achievement
    public void UnlockAcievement(string id)
    {
        Social.ReportProgress(id, 100, success => { });
    }

    public void ShowAcievementsUI()
    {
        Social.ShowAchievementsUI();
    }
    #endregion

    #region Leaderboard
    public void AddScoreToLeaderBoard(string leaderboardId, long score)
    {
        Social.ReportScore(score, leaderboardId, success => { });
    }

    public void ShowLeaderboardsUI()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
