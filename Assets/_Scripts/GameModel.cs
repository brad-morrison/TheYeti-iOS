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
    public float deviceScreenHeight;
    // gameplay variables
    public int score;
    public int highScore;
    public float difficultyMultiplier;
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
    // flags
    public bool allowInput;
    public bool gameOver;
    public bool newHighScore;
    // GoldMode
    
    // StateMachines
    public GameObject stateMachines;
    public PlayMakerFSM FSM_GameOverAnimations;
    public PlayMakerFSM FSM_GoldModeAnimations;
    // others

    // scripts
    public Hikers hikers;
    
    
    private void Awake() {
        // script references 
        hikers = GetComponent<Hikers>();

        lifeBar_ScrollSpeed = -1.2f;
        deviceScreenWidth = Display.main.systemWidth;
        deviceScreenHeight = Display.main.systemHeight;
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
        score = score + amount;

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
