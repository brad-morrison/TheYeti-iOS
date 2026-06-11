using UnityEngine;

public class GameplayVariables : TheYeti
{
    // all controllable gameplay variables are located in this script, this
    // will make tweaking and editing the gameplay easier in future updates if needed.

    // gameplay
    [Header("Gameplay Controls")]
    public float difficultyIncreaseStep = 0.028f; // the higher the number, the faster the lifebar shrinks
    public float baseDifficulty = 0.2f;

    // gold mode
    [Header("Gold Mode Controls")]
    public float goldModeWaitTime_min = 5f; // minimum time between gold mode face spawn
    public float goldModeWaitTime_max = 20f; // max time between gold mode face spawn
    public float goldModeFaceSpeed = 1.5f; // the speed the gold mode face moves across the screen
    public float goldModeDurationSpeed = 2f; // how much to increase lifebar speed by multiplying, when it reaches 0 gold mode ends (2 = twice as fast)

    // frenzy mode
    [Header("Frenzy Mode Controls")]
    public int frenzyHikerChance = 20; // the chance that a hiker spawns tagged with frenzy mode [larger numer = less chance]
    public float frenzyTimeDuration; // the amount of time that frenzy mode lasts

    // visual

    private void Reset()
    {
        difficultyIncreaseStep = 0.028f;
        baseDifficulty = 0.2f;
        goldModeWaitTime_min = 5f;
        goldModeWaitTime_max = 20f;
        goldModeFaceSpeed = 1.5f;
        goldModeDurationSpeed = 2f;
        frenzyHikerChance = 20;
        frenzyTimeDuration = 0f;
    }
}
