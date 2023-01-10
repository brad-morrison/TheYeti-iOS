using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.SocialPlatforms.Impl;

public class Leaderboards : MonoBehaviour
{
    private ILeaderboard leaderboard;
    private string leaderboard_id_bestscore = "bestscore";
    private string leaderboard_id_kills = "kills";

    void Start()
    {

        // Authenticate user first
        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                Debug.Log(userInfo);
            }
            else
                Debug.Log("Authentication failed");
        });

        // create social leaderboard
        leaderboard = Social.CreateLeaderboard();
        leaderboard.id = leaderboard_id_bestscore;
        leaderboard.LoadScores(result =>
        {
            Debug.Log("Received " + leaderboard.scores.Length + " scores");
            foreach (IScore score in leaderboard.scores)
                Debug.Log(score);
        });

        // create social leaderboard
        leaderboard = Social.CreateLeaderboard();
        leaderboard.id = leaderboard_id_kills;
        leaderboard.LoadScores(result =>
        {
            Debug.Log("Received " + leaderboard.scores.Length + " scores");
            foreach (IScore score in leaderboard.scores)
                Debug.Log(score);
        });
    }

    public void ReportScore(long score, string leaderboardID)
    {
        Debug.Log("Reporting score " + score + " on leaderboard " + leaderboardID);
        Social.ReportScore(score, leaderboardID, success => {
            Debug.Log(success ? "Reported score successfully" : "Failed to report score");
        });
    }

    public void OpenLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
}
