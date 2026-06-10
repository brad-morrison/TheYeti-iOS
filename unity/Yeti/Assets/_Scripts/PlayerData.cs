using UnityEngine;
using System.Collections;

public class PlayerData: TheYeti
{
    private const string HighScoreKey = "high_score";
    private const string KillsKey = "kills";
    private const string LegacyTotalKillsKey = "total_kills";
    private const string MusicKey = "music";
    private const string SfxKey = "sfx";
    private const string CostumeKey = "costume";
    private const string LegacyCostumeKey = "current_costume";
    private const string PlayedCountKey = "playedCount";
    private const string TimesAppOpenedKey = "timesAppOpened";
    private const string GlobalUnlockKey = "global_unlock";

	// data variables
    public int highScore, totalKills;
    public int musicOn, sfxOn;
    public int purchasedAds, purchasedCostumes;
    public int currentCostume;
    public int timesPlayed;
    public int timesAppOpened;
    public int globalUnlock;

	void Awake()
	{
        GetAllPrefs();
    }

    // get all for debug
    public void GetAllPrefs()
    {
        highScore = GetHighScore();
        totalKills = GetKills();
        musicOn = PlayerPrefs.GetInt(MusicKey);
        sfxOn = PlayerPrefs.GetInt(SfxKey);
        currentCostume = GetCostume();
        timesPlayed = PlayerPrefs.GetInt(PlayedCountKey);
        timesAppOpened = PlayerPrefs.GetInt(TimesAppOpenedKey);
        globalUnlock = PlayerPrefs.GetInt(GlobalUnlockKey);
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
        PlayerPrefs.SetInt(TimesAppOpenedKey, timesAppOpened);
        PlayerPrefs.Save();
    }

    // gets
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey);
    }

    public int GetKills()
    {
        if (!PlayerPrefs.HasKey(KillsKey) && PlayerPrefs.HasKey(LegacyTotalKillsKey))
        {
            return PlayerPrefs.GetInt(LegacyTotalKillsKey);
        }

        return PlayerPrefs.GetInt(KillsKey);
    }

    public int GetCostume()
    {
        if (!PlayerPrefs.HasKey(CostumeKey) && PlayerPrefs.HasKey(LegacyCostumeKey))
        {
            return PlayerPrefs.GetInt(LegacyCostumeKey);
        }

        return PlayerPrefs.GetInt(CostumeKey);
    }

    public bool GetMusic()
    {
        return PlayerPrefs.GetInt(MusicKey) == 1;
    }

    public bool GetSfx()
    {
        return PlayerPrefs.GetInt(SfxKey) == 1;
    }

    // sets
    public void SetHighScore(int value)
    {
        highScore = value;
        PlayerPrefs.SetInt(HighScoreKey, value);
        PlayerPrefs.Save();
    }

    public void SetKills(int value)
    {
        totalKills = value;
        PlayerPrefs.SetInt(KillsKey, value);
        Debug.Log("set kills to " + value);
        PlayerPrefs.Save();
    }

    public void SetMusic(bool value)
    {
        musicOn = value ? 1 : 0;
        PlayerPrefs.SetInt(MusicKey, musicOn);
        PlayerPrefs.Save();
    }

    public void SetSfx(bool value)
    {
        sfxOn = value ? 1 : 0;
        PlayerPrefs.SetInt(SfxKey, sfxOn);
        PlayerPrefs.Save();
    }

    public void SetCostume(int value)
    {
        currentCostume = value;
        PlayerPrefs.SetInt(CostumeKey, value);
        PlayerPrefs.Save();
    }

    public void PlayedCountAdd()
    {
        timesPlayed++;
        PlayerPrefs.SetInt(PlayedCountKey, timesPlayed);
        PlayerPrefs.Save();
    }

    public bool IsGlobalUnlocked()
    {
        return globalUnlock != 0;
    }

    public void SetGlobalUnlockCheat(bool val) {
        if (val)
        {
            PlayerPrefs.SetInt(GlobalUnlockKey, 1);
            globalUnlock = 1;
            print("unlocked all costumes cheat activated");
            
        }
        else 
        {
            PlayerPrefs.SetInt(GlobalUnlockKey, 0);
            globalUnlock = 0;
        }

        PlayerPrefs.Save();
    }

    // for debug //

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            print("resetting high scores");
            // reset playerprefs
            SetHighScore(0);
            SetKills(0);
            // reset on apple server
            GM.leaderboards.SendScores(0, 0);

        }
    }
#endif
}
