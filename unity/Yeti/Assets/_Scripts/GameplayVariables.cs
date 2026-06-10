using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayVariables : TheYeti
{
    // all controllable gameplay variables are located in this script, this
    // will make tweaking and editing the gameplay easier in future updates if needed.

    // gameplay
    [Header("Gameplay Controls")]
    public float difficultyIncreaseStep; // the higher the number, the faster the lifebar shrinks
    public float baseDifficulty;

    // gold mode
    [Header("Gold Mode Controls")]
    public float goldModeWaitTime_min; // minimum time between gold mode face spawn
    public float goldModeWaitTime_max; // max time between gold mode face spawn
    public float goldModeFaceSpeed; // the speed the gold mode face moves across the screen
    public float goldModeDurationSpeed; // how much to increase lifebar speed by multiplying, when it reaches 0 gold mode ends (2 = twice as fast)

    // frenzy mode
    [Header("Frenzy Mode Controls")]
    public int frenzyHikerChance; // the chance that a hiker spawns tagged with frenzy mode [larger numer = less chance]
    public float frenzyTimeDuration; // the amount of time that frenzy mode lasts

    // visual

    private void Start()
    {
        // these values are for reference
        // current values are stored in the editor
        // -------
        frenzyHikerChance = 20;
        goldModeWaitTime_min = 5;
        goldModeWaitTime_max = 20;
        difficultyIncreaseStep = 0.028f;
        baseDifficulty = 0.2f;
        goldModeFaceSpeed = 1.5f;
        goldModeDurationSpeed = 2;
    }
}

