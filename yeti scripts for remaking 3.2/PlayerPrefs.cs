using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int playerHighScore;
    public int playerKills;
    public int unlockedCostumes;
    public int removedAds;

    // player pref keys
    // ----------------
    // high score = "high_score"
    // kills = "kills"
    // unlockedCostumes = "purchase_costumes"
    // removeAds = "purchase_ads"

    // Start is called before the first frame update
    void Awake()
    {
        RefreshPrefs();
    }

    public int GetPlayerPrefInt(string key)
    {
        int output = PlayerPrefs.GetInt(key, 0);
        return output;
    }

    public void RefreshPrefs()
    {
        playerHighScore = PlayerPrefs.GetInt("high_score", 0);
        playerKills = PlayerPrefs.GetInt("kills", 0);
        unlockedCostumes = PlayerPrefs.GetInt("purchase_costumes", 0);
        removedAds = PlayerPrefs.GetInt("purchase_ads", 0);
    }

    
}
