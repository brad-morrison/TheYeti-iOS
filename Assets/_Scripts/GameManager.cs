using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    // DEBUG
    [Header("FOR TESTING")]
    public bool noTimerDeath;
    public bool noTouchDeath;
    public bool sound;

    // device
    [Header("")]
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
    public int totalKills_counter;
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
        deviceScreenWidth = Display.main.systemWidth;
        deviceScreenHeight = Display.main.systemHeight;
        // costume
        costumesList = costumesListPrefab.GetComponent<Costumes>().costumesList;
        allowInput = true;
        isGameOver = false;
        newHighScore = false;
    }

    private void Start()
    {
        // set debug data
        noTimerDeath = true;
        noTouchDeath = true;
        sound = false;


        // get score data
        highScore = PlayerPrefs.GetInt("high_score", 0);
        Debug.Log("high score - " + highScore);
        totalKills = PlayerPrefs.GetInt("total_kills", 0);
        Debug.Log("total kills - " + totalKills);

        score = 0;
        lifeBar_ScrollSpeed = -1.2f;
        hikers.InitHikers();
        hikers.SpawnHiker();
        CalculateNextGoldModeSpawn();
    }

    public void HandleInput(string command) {

        if (goldMode.goldMode) { 
            audio.PlaySound(audio.coin);
            goldMode.multiplierPop.GetComponent<TextMeshPro>().text = "x" + goldMode.goldModeMultiplier.ToString();
            Instantiate(goldMode.multiplierPop);
            }

        switch(command) {
            case "left":
            yeti.SetSprite(0);
            if (IsPlayerCorrect(0))
            {
                totalKills_counter++;
                audio.PlaySound(audio.punchSmall);
                SetScore(AddToScore());
                SetScoreUI();
                lifebar.PunchScale();
                hikers.KillHiker();
            }
            else
            {
                // add totalKills from this game to total
                PlayerPrefs.SetInt("total_kills", totalKills + totalKills_counter);
                if (!noTouchDeath) // for debug
                    gameOver.SetGameOver();
            }
            break;

            case "right":
            yeti.SetSprite(2);
            if (IsPlayerCorrect(1))
            {
                totalKills_counter++;
                audio.PlaySound(audio.punchLarge);
                SetScore(AddToScore());
                SetScoreUI();
                lifebar.PunchScale();
                hikers.KillHiker();
            }
            else
            {
                // add totalKills from this game to total
                PlayerPrefs.SetInt("total_kills", totalKills + totalKills_counter);
                if (!noTouchDeath) // for debug
                    gameOver.SetGameOver();
            }
            break;

            default:
            break;
        }
    }

    public int AddToScore() {
        if (goldMode.goldMode) {
            return goldMode.goldModeMultiplier;
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

    public void CalculateNextGoldModeSpawn()
    {
        // choose a random time to wait until the next gold mode
        float time = Random.Range(1.0f, 5.0f);
        // call the coroutine
        StartCoroutine(GoldModeFaceCountdown(time));
    }

    public IEnumerator GoldModeFaceCountdown(float from)
    {
        Debug.Log("Gold face spawning in " + from + " seconds");
        yield return new WaitForSeconds(from);
        // spawn gold face
        Instantiate(goldMode.goldModeFace);
    }

}