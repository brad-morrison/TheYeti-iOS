using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : GameElement
{
    // device
    public float deviceScreenWidth;
    // yeti
    public GameObject yeti, yeti_shadow, yeti_goldOutline;
    public Sprite yeti_left, yeti_right, yeti_bothUp, yeti_bothDown1, yeti_bothDown2, yeti_dead;
    public Sprite yetiGold_left, yetiGold_right, yetiGold_bothUp;
    public GameObject goldYeti;
    public GameObject goldFlames;
    // gameplay variables
    public int score;
    public float yetiPunchInterval;
    public float hikerOffset;
    public float hikerSpacing;
    public float difficultyMultiplier;
    public float goldModeLength;
    public int goldModeMultiplier;
    // ui
    public GameObject text_score;
    public LifeBar lifebar;
    public float lifeBar_ScrollSpeed;
    // prefabs
    public GameObject hiker;
    // markers
    public GameObject spawnPoint;
    public GameObject activeHiker;
    // flags
    public bool goldMode;
    // others
    public List<GameObject> hikers = new List<GameObject>();
    

    private void Awake() {
        lifeBar_ScrollSpeed = -1.2f;
        deviceScreenWidth = Display.main.systemWidth;
    }

    public void SetScore(int amount)
    {
        score = amount;
    }
}
