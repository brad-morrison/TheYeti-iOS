using UnityEngine;

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
        Initialize();
    }

    public void Initialize()
    {
        MigrateLegacyPrefs();
        RefreshCachedValues();

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

        IncrementTimesAppOpened();
        LogCachedValues();
    }

    // get all for debug
    public void GetAllPrefs()
    {
        RefreshCachedValues();
        LogCachedValues();
    }

    // gets
    public int GetHighScore()
    {
        return GetInt(HighScoreKey);
    }

    public int GetKills()
    {
        return GetInt(KillsKey);
    }

    public int GetCostume()
    {
        return GetInt(CostumeKey);
    }

    public bool GetMusic()
    {
        return GetBool(MusicKey);
    }

    public bool GetSfx()
    {
        return GetBool(SfxKey);
    }

    // sets
    public void SetHighScore(int value)
    {
        highScore = value;
        SetInt(HighScoreKey, value);
    }

    public void SetKills(int value)
    {
        totalKills = value;
        SetInt(KillsKey, value);
        Debug.Log("set kills to " + value);
    }

    public void SetMusic(bool value)
    {
        musicOn = value ? 1 : 0;
        SetBool(MusicKey, value);
    }

    public void SetSfx(bool value)
    {
        sfxOn = value ? 1 : 0;
        SetBool(SfxKey, value);
    }

    public void SetCostume(int value)
    {
        currentCostume = value;
        SetInt(CostumeKey, value);
    }

    public void PlayedCountAdd()
    {
        timesPlayed++;
        SetInt(PlayedCountKey, timesPlayed);
    }

    public bool IsGlobalUnlocked()
    {
        return globalUnlock != 0;
    }

    public void SetGlobalUnlockCheat(bool val) {
        if (val)
        {
            SetInt(GlobalUnlockKey, 1);
            globalUnlock = 1;
            print("unlocked all costumes cheat activated");
            
        }
        else 
        {
            SetInt(GlobalUnlockKey, 0);
            globalUnlock = 0;
        }
    }

    private void MigrateLegacyPrefs()
    {
        MigrateInt(LegacyTotalKillsKey, KillsKey);
        MigrateInt(LegacyCostumeKey, CostumeKey);
    }

    private void MigrateInt(string legacyKey, string currentKey)
    {
        if (PlayerPrefs.HasKey(currentKey) || !PlayerPrefs.HasKey(legacyKey))
            return;

        PlayerPrefs.SetInt(currentKey, PlayerPrefs.GetInt(legacyKey));
        PlayerPrefs.Save();
    }

    private void RefreshCachedValues()
    {
        highScore = GetHighScore();
        totalKills = GetKills();
        musicOn = GetInt(MusicKey);
        sfxOn = GetInt(SfxKey);
        currentCostume = GetCostume();
        timesPlayed = GetInt(PlayedCountKey);
        timesAppOpened = GetInt(TimesAppOpenedKey);
        globalUnlock = GetInt(GlobalUnlockKey);
    }

    private void IncrementTimesAppOpened()
    {
        timesAppOpened++;
        SetInt(TimesAppOpenedKey, timesAppOpened);
    }

    private void LogCachedValues()
    {
        Debug.Log(
            "highScore - " + highScore + "      " +
            "totalKills - " + totalKills + "      " +
            "music - " + musicOn + "      " +
            "sfx - " + sfxOn + "      " +
            "currentCostume - " + currentCostume + "      " +
            "times played = " + timesPlayed + "       " +
            "times app opened = " + timesAppOpened + "     "
        );
    }

    private int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    private bool GetBool(string key)
    {
        return GetInt(key) == 1;
    }

    private void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    private void SetBool(string key, bool value)
    {
        SetInt(key, value ? 1 : 0);
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
