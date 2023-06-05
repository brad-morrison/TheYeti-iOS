using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;
//using static UnityEngine.GraphicsBuffer;

public class GameManager : TheYeti {
    // DEBUG
    [Header("FOR TESTING")]
    public bool noTimerDeath;
    public bool noTouchDeath;
    public bool sound;
    public bool allowGoldMode;      // bool in if statement of goldfacecalculator
    public bool allowFrenzyMode;    // bool in hikers script if statement

    // device
    [Header("")]
    public float deviceScreenWidth;
    public float deviceScreenHeight;
    // scripts
    public Hikers hikers;
    public Yeti yeti;
    public GoldMode goldMode;
    public GameOver gameOver;
    //public Particles particles;
    public FrenzyMode frenzyMode;
    public Weather weather;
    public Sky sky;
    // gameplay
    [Header("Gameplay")]
    public int frenzyHikerChance; // large number = less chance
    public float goldModeWaitTime_min; // min wait time for face spawn
    public float goldModeWaitTime_max; // max wait time for face spawn
    [Header("")]
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
    public GameObject timer;
    // flags
    public bool allowInput;
    public bool isGameOver;
    public bool newHighScore;
    // costumes
    public GameObject costumesListPrefab;
    public List<Costume> costumesList;
    // character
    public GameObject yetiCharacter, yetiCharacter_gameOver;
    // effects
    public GameObject fallingBones;
    public bool fallingBonesIsOn;
    public GameObject newHighScore_text;
    public GameObject smashLeft, smashRight;
    // events
    public UnityEvent scoreBounceSmall = new UnityEvent();
    public UnityEvent scoreBounceBig = new UnityEvent();
    public UnityEvent cameraShake = new UnityEvent();
    public UnityEvent platformShake = new UnityEvent();
    public UnityEvent yetiShake = new UnityEvent();
    public UnityEvent _newHighScore = new UnityEvent();



    private void Awake() {
        deviceScreenWidth = Display.main.systemWidth;
        deviceScreenHeight = Display.main.systemHeight;

        GM.SceneChanged();

        // costume
        costumesList = costumesListPrefab.GetComponent<Costumes>().costumesList;
        allowInput = true;
        isGameOver = false;
        newHighScore = false;

        
    }

    private void Start()
    {
        // setting score to test game centre
        totalKills_counter = 5000;

        // set debug data
        if (Application.isEditor)
        {
            //noTimerDeath = true;
            //noTouchDeath = true;
            //sound = false;
        }

        // get score data
        highScore = GM.playerData.GetHighScore();

        score = 0;
        lifeBar_ScrollSpeed = -0.5f;
        hikers.InitHikers();
        hikers.SpawnHiker();
        CalculateNextGoldModeSpawn();

        FallingBones(false);
    }

    public void HandleInput(string command) {

        //cameraShake.Invoke();
        platformShake.Invoke();
        yetiShake.Invoke();

        if (goldMode.goldMode) { 
            GM.audio.PlaySound(GM.audio.coin);
            goldMode.multiplierPop.GetComponent<TextMeshPro>().text = "x" + goldMode.goldModeMultiplier.ToString();

            scoreBounceBig.Invoke();
            
            Instantiate(goldMode.multiplierPop);
        }

        Hit(command);
    }

    public void Hit(string side)
    {
        iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)(goldMode.goldMode ? 2 : 1));

        if (!goldMode.goldMode)
            scoreBounceSmall.Invoke();

        if (frenzyMode.frenzyMode)
            yeti.SetSprite("idle1");
        else
            yeti.SetSprite(side);

        /*
        if (frenzyMode.frenzyMode)
            cameraShake.Invoke();
        */

        SmashEffect(side);

        if (IsPlayerCorrect(side))
        {
            totalKills_counter++;
            GM.audio.PlaySound(GM.audio.punchSmall);
            AddToScore();
            SetScoreUI();
            lifebar.PunchScale();
            // frenzy check
            if (!frenzyMode.frenzyMode)
                frenzyMode.FrenzyCheck();

            hikers.KillHiker();
            // play hiker death sound
            int rand = Random.Range(1, 6);
            if (rand == 1) GM.audio.PlaySound(GM.audio.hikerDeath1);
            if (rand == 2) GM.audio.PlaySound(GM.audio.hikerDeath2);
            if (rand == 3) GM.audio.PlaySound(GM.audio.hikerDeath3);
            // check for difficulty increase
            DifficultyIncreaseCheck();
        }
        else
        {
            if (!noTouchDeath) // for debug
            {
                HandleFinalScores();
                gameOver.SetGameOver();
            }
        }
    }

    public bool IsPlayerCorrect(string side)
    {
        if (frenzyMode.frenzyMode)
            return true;

        if (!hikers.activeHiker.GetComponent<Hiker>().left && side == "left")
            return true;

        if (hikers.activeHiker.GetComponent<Hiker>().left && side == "right")
            return true;

        return false;
    }

    public void AddToScore() {
        int amount;

        // amount to add to score depending on mode
        if (goldMode.goldMode)
            amount = goldMode.goldModeMultiplier;
        else 
            amount = 1;

        // set score
        score += amount;

        // check for new high score so that player can be alerted
        if (score > highScore)
        {
            highScore = score;
            AlertNewHighScore(); // this has no logic attached, just visual things for the player
        }

    }

    public void HandleFinalScores()
    {
        // high score
        if (score > GM.playerData.GetHighScore())
        {
            newHighScore = true;
            GM.playerData.SetHighScore(score);
        }

        // kills
        int totalKills_final = GM.playerData.GetKills() + totalKills_counter;
        GM.playerData.SetKills(totalKills_final);

        // publish to leaderboards
        GM.leaderboards.SendScores(GM.playerData.GetHighScore(), totalKills_final);
    }


    public void AlertNewHighScore()
    {
        // effects
        StartCoroutine(FallingBonesFor(1.0f));
        newHighScore_text.SetActive(true);
        //audio.PlaySound(audio.high_score);
        
    }

    public void DifficultyIncreaseCheck()
    {
        if (score % 10 == 0)
        {
            // ends in 0
            difficultyMultiplier += 0.02f;
            lifeBar_ScrollSpeed -= 0.1f;

            print(difficultyMultiplier);
        }
        else
        {
            return;
        }
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

    public void FallingBones(bool isOn)
    {
        Component[] spawners = fallingBones.GetComponentsInChildren<BoneSpawner>();

        foreach (BoneSpawner spawner in spawners)
        {
            if (isOn)
                spawner.spawn = true;
            else
                spawner.spawn = false;
        }

    }

    public void SmashEffect(string side)
    {
        if (frenzyMode.frenzyMode)
        {
            Instantiate(smashLeft);
            Instantiate(smashRight);
            return;
        }

        if (side == "left")
            Instantiate(smashLeft);
        else
            Instantiate(smashRight);
    }

    public void SetScoreUI() {
        text_score.GetComponent<TextMeshPro>().text = score == 0 ? "o" : score.ToString();
    }

    public void CalculateNextGoldModeSpawn()
    {
        // choose a random time to wait until the next gold mode
        float time = Random.Range(goldModeWaitTime_min, goldModeWaitTime_max);
        // call the coroutine
        StartCoroutine(GoldModeFaceCountdown(time));
    }

    public IEnumerator GoldModeFaceCountdown(float from)
    {
        Debug.Log("Gold face spawning in " + from + " seconds");
        yield return new WaitForSeconds(from);
        // spawn gold face
        if (!isGameOver && !frenzyMode.frenzyMode && allowGoldMode)
            Instantiate(goldMode.goldModeFace);
    }

    public IEnumerator FallingBonesFor(float time)
    {
        FallingBones(true);
        yield return new WaitForSeconds(time);
        FallingBones(false);
    }

    

}

