using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;
//using static UnityEngine.GraphicsBuffer;

public enum PunchSide
{
    Left,
    Right
}

public class GameManager : TheYeti {
    private const float StartingLifeBarScrollSpeed = -0.5f;
    private const int DifficultyIncreaseScoreInterval = 10;
    private const float LifeBarScrollSpeedDifficultyStep = 0.1f;

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
    public GameplayVariables gameplayVariables;
    public Hikers hikers;
    public Yeti yeti;
    public GoldMode goldMode;
    public GameOver gameOver;
    public Tutorial tutorial;
    //public Particles particles;
    public FrenzyMode frenzyMode;
    public Weather weather;
    public Sky sky;

    [Header("")]
    // model variables
    public int score;
    public int highScore;
    public int totalKills_counter;
    public int totalKills;
    public float difficulty;
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
    private TextMeshPro scoreText;
    private GameSession session;

    public bool IsGoldModeActive { get { return goldMode.goldMode; } }
    public bool IsFrenzyModeActive { get { return frenzyMode.frenzyMode; } }
    public bool IsGameOver { get { return isGameOver || (gameOver != null && gameOver.gameOver); } }
    public bool CanTagFrenzyHiker { get { return !IsGoldModeActive && !IsFrenzyModeActive && allowFrenzyMode; } }
    public bool CanSpawnGoldModeFace { get { return !IsGameOver && !IsFrenzyModeActive && allowGoldMode; } }


    private void Awake() {
        deviceScreenWidth = Display.main.systemWidth;
        deviceScreenHeight = Display.main.systemHeight;
        scoreText = text_score.GetComponent<TextMeshPro>();

        // costume
        costumesList = costumesListPrefab.GetComponent<Costumes>().costumesList;
        allowInput = true;
        isGameOver = false;
        newHighScore = false;

        // increment play counter
        GM.playerData.PlayedCountAdd();
    }

    private void Start()
    {
        // set debug data
        if (Application.isEditor)
        {
            //noTimerDeath = true;
            //noTouchDeath = true;
            //sound = false;
        }

        session = new GameSession(GM.playerData.GetHighScore());
        SyncSessionState();
        lifeBar_ScrollSpeed = StartingLifeBarScrollSpeed;
        hikers.InitHikers();
        hikers.SpawnHiker();
        CalculateNextGoldModeSpawn();

        FallingBones(false);

        // check for tutorial
        if (GM.playerData.timesPlayed > 0)
        {
            tutorial.ActivateTutorial();
        }

        // set difficulty
        difficulty = gameplayVariables.baseDifficulty;
    }

    public void HandleInput(PunchSide side)
    {
        PlayInputFeedback();
        PlayGoldModeHitFeedback();
        Hit(side);
    }

    public void PlayInputFeedback()
    {
        platformShake.Invoke();
        yetiShake.Invoke();
    }

    public void PlayGoldModeHitFeedback()
    {
        if (!IsGoldModeActive)
            return;

        GM.audio.PlaySound(GM.audio.coin);

        scoreBounceBig.Invoke();
        goldMode.ShowMultiplierPop();
    }

    public void Hit(PunchSide side)
    {

        if (!IsGoldModeActive)
            scoreBounceSmall.Invoke();

        ApplyYetiHitPose(side);
        SmashEffect(side);

        if (IsPlayerCorrect(side))
        {
            HandleCorrectHit();
        }
        else
        {
            HandleMissedHit();
        }
    }

    public void ApplyYetiHitPose(PunchSide side)
    {
        if (IsFrenzyModeActive)
        {
            yeti.SetSprite(YetiPose.Idle1);
            return;
        }

        yeti.SetSprite(side);
    }

    public bool IsPlayerCorrect(PunchSide side)
    {
        if (IsFrenzyModeActive)
            return true;

        bool hikerIsLeft = hikers.activeHiker.GetComponent<Hiker>().left;
        return hikerIsLeft ? side == PunchSide.Right : side == PunchSide.Left;
    }

    public void HandleCorrectHit()
    {
        session.RegisterKill();
        SyncSessionState();
        print("total kills in this game - " + totalKills_counter);
        GM.audio.PlaySound(GM.audio.punchSmall);
        AddToScore();
        SetScoreUI();
        lifebar.PunchScale();

        if (!IsFrenzyModeActive)
            frenzyMode.FrenzyCheck();

        hikers.KillHiker();
        PlayRandomHikerDeathSound();
        DifficultyIncreaseCheck();
    }

    public void HandleMissedHit()
    {
        if (noTouchDeath)
            return;

        HandleFinalScores();
        gameOver.SetGameOver();
    }

    public void AddToScore() {

        bool isNewHighScore = session.AddScore(ScoreIncrement());
        SyncSessionState();

        if (isNewHighScore)
        {
            AlertNewHighScore(); // this has no logic attached, just visual things for the player
        }

    }

    public int ScoreIncrement()
    {
        if (IsGoldModeActive)
            return goldMode.goldModeMultiplier;

        return 1;
    }

    public void HandleFinalScores()
    {
        // high score
        if (session.ShouldSaveHighScore(GM.playerData.GetHighScore()))
        {
            newHighScore = true;
            GM.playerData.SetHighScore(session.Score);
        }

        // kills
        int storedKills = GM.playerData.GetKills();
        int totalKills_final = session.TotalKillsAfterRound(storedKills);
        print("kills from pp - " + storedKills + " kill counter - " + totalKills_counter);
        print("setting kills to " + totalKills_final);
        GM.playerData.SetKills(totalKills_final);

        // publish to leaderboards
        GM.leaderboards.SendScores(GM.playerData.GetHighScore(), totalKills_final);
    }

    public void PlayRandomHikerDeathSound()
    {
        int rand = Random.Range(1, 6);
        if (rand == 1) GM.audio.PlaySound(GM.audio.hikerDeath1);
        if (rand == 2) GM.audio.PlaySound(GM.audio.hikerDeath2);
        if (rand == 3) GM.audio.PlaySound(GM.audio.hikerDeath3);
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
        if (score % DifficultyIncreaseScoreInterval == 0)
        {
            // ends in 0
            difficulty += gameplayVariables.difficultyIncreaseStep;
            lifeBar_ScrollSpeed -= LifeBarScrollSpeedDifficultyStep; // controls speed of the scrolling animation, not speed of shrinkage
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
            spawner.spawn = isOn;
        }

    }

    public void SmashEffect(PunchSide side)
    {
        if (IsFrenzyModeActive)
        {
            Instantiate(smashLeft);
            Instantiate(smashRight);
            return;
        }

        if (side == PunchSide.Left)
            Instantiate(smashLeft);
        else
            Instantiate(smashRight);
    }

    public void LifebarState(bool value)
    {
        lifebar.animate = value;
    }

    public void SetScoreUI() {
        scoreText.text = FormatScore(score);
    }

#if UNITY_EDITOR
    public void ResetDebugScores()
    {
        GM.playerData.SetHighScore(0);
        GM.playerData.SetKills(0);
        session.Reset(0);
        SyncSessionState();
        SetScoreUI();
    }

    public void AddDebugHighScore(int amount)
    {
        int updatedHighScore = GM.playerData.GetHighScore() + amount;
        GM.playerData.SetHighScore(updatedHighScore);
        session.OverrideHighScore(updatedHighScore);
        SyncSessionState();
    }

    public void AddDebugKills(int amount)
    {
        int updatedKills = GM.playerData.GetKills() + amount;
        GM.playerData.SetKills(updatedKills);
        totalKills = updatedKills;
    }
#endif

    private void SyncSessionState()
    {
        score = session.Score;
        highScore = session.HighScore;
        totalKills_counter = session.RoundKills;
        newHighScore = session.HasNewHighScore;
    }

    public static string FormatScore(int value)
    {
        return value == 0 ? "o" : value.ToString();
    }

    public void CalculateNextGoldModeSpawn()
    {
        // choose a random time to wait until the next gold mode
        float time = Random.Range(gameplayVariables.goldModeWaitTime_min, gameplayVariables.goldModeWaitTime_max);
        // call the coroutine
        StartCoroutine(GoldModeFaceCountdown(time));
    }

    public IEnumerator GoldModeFaceCountdown(float from)
    {
        Debug.Log("Gold face spawning in " + from + " seconds");
        yield return new WaitForSeconds(from);
        // spawn gold face
        if (CanSpawnGoldModeFace)
            Instantiate(goldMode.goldModeFace);
    }

    public IEnumerator FallingBonesFor(float time)
    {
        FallingBones(true);
        yield return new WaitForSeconds(time);
        FallingBones(false);
    }

    

}
