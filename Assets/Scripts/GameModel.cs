using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : GameElement
{
    // yeti
    public GameObject yeti;
    public Sprite yeti_left, yeti_right, yeti_bothUp, yeti_bothDown1, yeti_bothDown2, yeti_dead;
    public GameObject goldYeti;
    public GameObject goldFlames;
    // gameplay variables
    public int score;
    public float yetiPunchInterval;
    public float hikerOffset;
    public float hikerSpacing;
    public float difficultyMultiplier;
    public float goldModeLength;
    // ui
    public GameObject text_score;
    public LifeBar lifebar;
    // prefabs
    public GameObject hikerRed;
    public GameObject hikerGreen;
    // markers
    public GameObject spawnPoint;
    public GameObject activeHiker;
    // flags
    public bool goldMode;
    // others
    public List<GameObject> hikers = new List<GameObject>();
    

    public void SetScore(int amount)
    {
        score = amount;
    }
}
