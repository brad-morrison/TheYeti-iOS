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
    public int totalKills;
    public float difficultyMultiplier;
    // ui
    public GameObject text_score;
    public GameObject finalScore;
    public GameObject finalBest;
    public LifeBar lifebar;
    public float lifeBar_ScrollSpeed;
    // flags
    public bool allowInput;
    public bool gameOver;
    public bool newHighScore;

    
    
    private void Awake() {
        lifeBar_ScrollSpeed = -1.2f;
        deviceScreenWidth = Display.main.systemWidth;
        deviceScreenHeight = Display.main.systemHeight;
        allowInput = true;
        gameOver = false;
        newHighScore = false;
        highScore = PlayerPrefs.GetInt("high_score", 0);

        // reset these
        highScore = 250;
        totalKills = 250;
    }

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
