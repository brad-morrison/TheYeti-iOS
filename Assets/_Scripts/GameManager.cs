using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    // device
    public float deviceScreenWidth;
    public float deviceScreenHeight;
    // scripts
    public Hikers hikers;
    public Yeti yeti;
    public GoldMode goldMode;
    public GameOver gameOver;
    public Audio audio;
    public UserInput input;
    public Particles particles;
    // model variables
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
    public bool isGameOver;
    public bool newHighScore;
    // costumes
    public GameObject costumesListPrefab;
    public List<Costume> costumesList;
    // character
    public GameObject yetiCharacter, yetiCharacter_gameOver;

    private void Awake() {
        
        // costume
        costumesList = costumesListPrefab.GetComponent<Costumes>().costumesList;

        
        lifeBar_ScrollSpeed = -1.2f;
        deviceScreenWidth = Display.main.systemWidth;
        deviceScreenHeight = Display.main.systemHeight;
        allowInput = true;
        isGameOver = false;
        newHighScore = false;
        highScore = PlayerPrefs.GetInt("high_score", 0);
    }

    private void Start()
    {
        score = 0;
        hikers.InitHikers();
        hikers.SpawnHiker();
    }

    public void HandleInput(string command) {

        if (goldMode.goldMode) { 
            audio.PlaySound(audio.coin); 
            Instantiate(goldMode.multiplierPop);
            }

        switch(command) {
            case "left":
            yeti.SetSprite(0);
            if (IsPlayerCorrect(0))
            {
                audio.PlaySound(audio.punchSmall);
                SetScore(AddToScore());
                SetScoreUI();
                lifebar.PunchScale();
                hikers.KillHiker();
            }
            else
            {
                gameOver.SetGameOver();
            }
            break;

            case "right":
            yeti.SetSprite(2);
            if (IsPlayerCorrect(1))
            {
                audio.PlaySound(audio.punchLarge);
                SetScore(AddToScore());
                SetScoreUI();
                lifebar.PunchScale();
                hikers.KillHiker();
            }
            else
            {
                gameOver.SetGameOver();
            }
            break;

            default:
            break;
        }
    }

    public int AddToScore() {
        if (goldMode.goldMode) {
            return goldMode.goldModeMultiplier + 1;
        } else {
            return 1;
        }
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

    public bool IsPlayerCorrect(int pos)
    {
        if (!hikers.activeHiker.GetComponent<Hiker>().left && pos == 0)
        {
            return true;
        }

        if (hikers.activeHiker.GetComponent<Hiker>().left && pos == 1)
        {
            return true;
        }

        return false;
    }

    public void ActivateGoldMode()
    {
        goldMode.GoldModeAnnounce();
    }

    public void DeactivateGoldMode()
    {
        
        goldMode.DeactivateGoldMode();
    }

    public void DisableAllAnimations() {

        // disable lifebar animations
        lifebar.animate = false;
        lifeBar_ScrollSpeed = 0;

        // disable hiker animations
        hikers.DisableAnimations();

     }

    public void SetScoreUI() {
        text_score.GetComponent<TextMeshPro>().text = score == 0 ? "o" : score.ToString();
    }
}