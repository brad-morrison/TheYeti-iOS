using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // script references
    YetiSprite yetiSprite, goldYetiSprite;
    SpawnHiker spawnHiker;
    GoldModeTicker goldModeTicker;
    Audio audio;
    Timer timer;
    Score score;
    GameOverScreen gameOverUI;
    GoldUI goldUI;
    HighScoreAnimations highScoreAnimations;
    SpawnHikerAtDeath spawnHikerAtDeath;
    public float DifficultyMultiplier;
    public int goldMultiplier = 1;
    public bool goldModeActivated = false;
    public bool newHighScore  = false;
    public bool dead = false;
    public GameObject inputColliders;
    public GameObject newHighScorePrefab;

    GameObject[] hikers = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("high_score", 10);
        //script refrences
        highScoreAnimations = GetComponent<HighScoreAnimations>();
        yetiSprite = GameObject.Find("yeti").GetComponent<YetiSprite>();
        spawnHiker = GetComponent<SpawnHiker>();
        score = GetComponent<Score>();
        timer = GameObject.Find("timer_scalar").GetComponent<Timer>();
        gameOverUI = GetComponent<GameOverScreen>();
        goldUI = GetComponent<GoldUI>();
        goldYetiSprite = goldUI.goldYeti.GetComponent<YetiSprite>();
        audio = GameObject.Find("Audio").GetComponent<Audio>();
        goldModeTicker = GetComponent<GoldModeTicker>();
        spawnHikerAtDeath = GetComponent<SpawnHikerAtDeath>();

        hikers[0] = spawnHiker.SpawnAt(0);
        hikers[1] = spawnHiker.SpawnAt(1);
        hikers[2] = spawnHiker.SpawnAt(2);
    }

    void UpdateHikers()
    {
        Destroy(hikers[0]);
        hikers[3] = spawnHiker.Spawn();

        // move objects
        hikers[3].GetComponent<Hiker>().MoveUp(spawnHiker.left3);
        hikers[2].GetComponent<Hiker>().MoveUp(spawnHiker.left2);
        hikers[1].GetComponent<Hiker>().MoveUp(spawnHiker.left1);

        // update array
        hikers[0] = hikers[1];
        hikers[1] = hikers[2];
        hikers[2] = hikers[3];
    }

    public void CheckForDeath(string userInput)
    {
        string correctSide = hikers[0].GetComponent<Hiker>().side;
        if ((userInput == "left" && correctSide == "left") || (userInput == "right" && correctSide == "right"))
        {
            //correct
            score.ScoreAdd(1 * goldMultiplier);
            if (goldModeActivated)
            {
                goldUI.CreateGoldFloatingText();
                score.PunchScale();
            }
            spawnHiker.SpawnDeadHiker(correctSide, hikers[0]);
            UpdateHikers();
            timer.PunchScale();
            goldModeTicker.CheckForGoldChance();
        }
        else
        {
            Death();
        }
    }

    public void Death()
    {
        dead = true;
        
        if (newHighScore)
        {
            highScoreAnimations.StopSpawns();
            score.highScore = score.scoreCount;
            PlayerPrefs.SetInt("high_score", score.scoreCount);
        }

        score.AddToKillsScore();
        inputColliders.SetActive(false);
        Invoke("OpenGameOverUI", 0.7f);

        // game over 'in-game' changes
        hikers[0].GetComponent<SpriteRenderer>().enabled = false;
        spawnHikerAtDeath.SpawnHiker(hikers[0].GetComponent<Hiker>().side, hikers[0].GetComponent<Hiker>().color);
        yetiSprite.StopAllCoroutines();
        yetiSprite.yetiRenderer.sprite = yetiSprite.death;
    }

    public void OpenGameOverUI()
    {
        gameOverUI.OpenUI();
        audio.Play(audio.gameOver);
    }
    public void UserInput(string side)
    {
        switch (side)
        {
            case "colliderLeft":
                yetiSprite.ChangeLeft();
                if (goldModeActivated)
                {
                    audio.Play(audio.goldPoint);
                    goldYetiSprite.ChangeLeft();
                }
                audio.Play(audio.punchLarge);
                CheckForDeath("left");
                break;


            case "colliderRight":
                yetiSprite.ChangeRight();
                if (goldModeActivated)
                {
                    audio.Play(audio.goldPoint);
                    goldYetiSprite.ChangeRight();
                }
                audio.Play(audio.punchSmall);
                CheckForDeath("right");
                break;
        }
    }

    public void StartGoldMode()
    {
        goldModeActivated = true;
        if (audio.on == 1)
        {
            audio.Play(audio.goldTapped);
            audio.Play(audio.goldUp);
        }
        int multiplierRoll = Random.Range(2,5);
        goldMultiplier = multiplierRoll;
        goldUI.TriggerGoldUI(goldMultiplier);
        score.scoreUI.GetComponent<TextMeshPro>().color = score.gold;
    }

    public void SetNewHighScore()
    {
        newHighScore = true;
        Instantiate(newHighScorePrefab, newHighScorePrefab.transform.position, Quaternion.identity);
        highScoreAnimations.StartSpawns();
    }

    public void EndGoldMode()
    {
        goldMultiplier = 1;
        goldModeActivated = false;
        if (!dead && audio.on == 1)
            audio.Play(audio.goldDown);
        goldUI.CloseGoldUI();
        goldYetiSprite.GetComponent<SpriteRenderer>().sprite = goldYetiSprite.bothUp;
        score.scoreUI.GetComponent<TextMeshPro>().color = Color.white;
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.A))
        {
            UserInput("colliderLeft");
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            UserInput("colliderRight");
        }

        
    }
}
