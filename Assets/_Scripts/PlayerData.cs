using UnityEngine;
using System.Collections;

public class PlayerData: TheYeti
{
	// data variables
    public int highScore, totalKills;
    public int musicOn, sfxOn;
    public int purchasedAds, purchasedCostumes;
    public int currentCostume;
    public int timesPlayed;
    public int timesAppOpened;

	void Awake()
	{
        GetAllPrefs();
    }

    // get all for debug
    public void GetAllPrefs()
    {
        highScore = PlayerPrefs.GetInt("high_score");
        totalKills = PlayerPrefs.GetInt("total_kills");
        musicOn = PlayerPrefs.GetInt("music");
        sfxOn = PlayerPrefs.GetInt("sfx");
        currentCostume = PlayerPrefs.GetInt("current_costume");
        timesPlayed = PlayerPrefs.GetInt("playedCount");
        timesAppOpened = PlayerPrefs.GetInt("timesAppOpened");
        Debug.Log(
            "highScore - " + highScore + "      " + 
            "totalKills - " + totalKills + "      " +
            "music - " + musicOn + "      " +
            "sfx - " + sfxOn + "      " +
            "currentCostume - " + currentCostume + "      " +
            "times played = " + timesPlayed + "       " +
            "times app opened = " + timesAppOpened + "     "

            );

        // check for first time opening
        if (timesAppOpened == 0)
        {
            // this is first time
            // do things here that I need to run on first open of app
            print("first time opened");
            musicOn = 1;
            sfxOn = 1;
            SetMusic(true);
            SetSfx(true);
        }

        // increment app opened counter
        timesAppOpened++;
        PlayerPrefs.SetInt("timesAppOpened", timesAppOpened);
        PlayerPrefs.Save();
    }

    // gets
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("high_score");
    }

    public int GetKills()
    {
        return PlayerPrefs.GetInt("kills");
    }

    public int GetCostume()
    {
        return PlayerPrefs.GetInt("costume");
    }

    public bool GetMusic()
    {
        if (PlayerPrefs.GetInt("music") == 1)
            return true;
        else
            return false;
    }

    public bool GetSfx()
    {
        if (PlayerPrefs.GetInt("sfx") == 1)
            return true;
        else
            return false;
    }

    // sets
    public void SetHighScore(int value)
    {
        PlayerPrefs.SetInt("high_score", value);
        PlayerPrefs.Save();
    }

    public void SetKills(int value)
    {
        PlayerPrefs.SetInt("kills", value);
        Debug.Log("set kills to " + value);
        PlayerPrefs.Save();
    }

    public void SetMusic(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt("music", 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
            PlayerPrefs.Save();
        }
    }

    public void SetSfx(bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt("sfx", 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("sfx", 0);
            PlayerPrefs.Save();
        }
    }

    public void SetCostume(int value)
    {
        PlayerPrefs.SetInt("costume", value);
        PlayerPrefs.Save();
    }

    public void PlayedCountAdd()
    {
        timesPlayed++;
        PlayerPrefs.SetInt("playedCount", timesPlayed);
        PlayerPrefs.Save();
    }

    // for debug //

    private void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            print("resetting high scores");
            // reset playerprefs
            SetHighScore(0);
            SetKills(0);
            // reset on apple server
            GM.leaderboards.SendScores(0, 0);

        }
    }
}

