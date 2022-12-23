using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HutongGames.PlayMaker;

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
    // hikers
    public Sprite hiker_red, hiker_red_down, hiker_red_axeUp, hiker_red_smiling;
    public List<GameObject> hikers = new List<GameObject>();
    public GameObject hiker_standing_left, hiker_standing_right;
    // gameplay variables
    public int score;
    public int highScore;
    public float yetiPunchInterval;
    public float hikerOffset;
    public float hikerSpacing;
    public float difficultyMultiplier;
    public float goldModeLength;
    public int goldModeMultiplier;
    // ui
    public GameObject text_score;
    public GameObject finalScore;
    public GameObject finalBest;
    public LifeBar lifebar;
    public float lifeBar_ScrollSpeed;
    public GameObject gameOverUI_Group;
    public GameObject gameOverUI_top;
    public GameObject gameOverUI_buttons;
    public GameObject gameOverUI_yeti;
    public GameObject gameOverUI_hiker;
    // prefabs
    public GameObject hiker;
    // markers
    public GameObject spawnPoint;
    public GameObject activeHiker;
    // flags
    public bool goldMode;
    public bool allowInput;
    public bool gameOver;
    public bool newHighScore;
    // StateMachines
    public GameObject stateMachines;
    public PlayMakerFSM FSM_GameOverAnimations;
    public PlayMakerFSM FSM_GoldModeAnimations;
    // others
    
    
    private void Awake() {
        lifeBar_ScrollSpeed = -1.2f;
        deviceScreenWidth = Display.main.systemWidth;
        allowInput = true;
        gameOver = false;
        newHighScore = false;
        highScore = PlayerPrefs.GetInt("high_score", 0);
        // statemachines
        FSM_GameOverAnimations = PlayMakerFSM.FindFsmOnGameObject(stateMachines, "GameOverAnimations");
        FSM_GoldModeAnimations = PlayMakerFSM.FindFsmOnGameObject(stateMachines, "GoldModeAnimations");
    }

    //public PlayMakerFSM 

    public void SetScore(int amount)
    {
        score = amount;

        if (score > highScore)
        {
            Debug.Log("new highscore! of " + score);
            newHighScore = true;
            highScore = score;
            PlayerPrefs.SetInt("high_score", highScore);
            PlayerPrefs.Save();
        }
    }
}
